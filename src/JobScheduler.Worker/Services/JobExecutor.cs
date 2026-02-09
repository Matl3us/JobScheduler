using System.Diagnostics;
using System.Text.Json;
using JobScheduler.Core.DTOs;
using JobScheduler.Core.Enums;
using JobScheduler.Core.Interfaces;
using JobScheduler.Core.Models;
using JobScheduler.Worker.Handlers;

namespace JobScheduler.Worker.Services;

public class JobExecutor(
    HttpRequestJobHandler httpHandler,
    IUnitOfWork unitOfWork
) : IJobExecutor
{
    public async Task<JobExecutionResult> ExecuteAsync(Job job, JobExecution execution)
    {
        execution.StartedAt = DateTime.UtcNow;
        execution.Status = JobExecutionStatus.Pending;
        await unitOfWork.SaveChangesAsync();

        var executionResult = new JobExecutionResult();
        var startTime = Stopwatch.GetTimestamp();

        try
        {
            switch (job.Type)
            {
                case JobType.HttpRequest:
                    var response = await httpHandler.ExecuteAsync(job.Data);
                    var options = new JsonSerializerOptions
                    {
                        IncludeFields = true
                    };
                    var serializedResponse = JsonSerializer.Serialize(response, options);

                    execution.Status = JobExecutionStatus.Completed;
                    execution.Result = serializedResponse;
                    executionResult.Result = serializedResponse;
                    executionResult.Success = true;

                    break;
            }
        }
        catch (Exception e)
        {
            if (e is ArgumentException)
            {
                executionResult.ErrorMessage = e.Message;
                execution.ErrorMessage = e.Message;
            }

            executionResult.Success = false;
            execution.Status = JobExecutionStatus.Failed;
        }

        var duration = Stopwatch.GetElapsedTime(startTime);

        execution.ExecutionDurationMs = duration.Milliseconds;
        execution.CompletedAt = DateTime.UtcNow;
        await unitOfWork.SaveChangesAsync();

        executionResult.Duration = duration;
        return executionResult;
    }
}