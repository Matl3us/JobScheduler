using JobScheduler.Core.DTOs;
using JobScheduler.Core.Models;

namespace JobScheduler.Core.Interfaces;

public interface IJobExecutor
{
    Task<JobExecutionResult> ExecuteAsync(Job job, JobExecution execution);
}