using AutoMapper;
using Cronos;
using FluentValidation;
using JobScheduler.Core.DTOs;
using JobScheduler.Core.Enums;
using JobScheduler.Core.Interfaces;
using JobScheduler.Core.Models;

namespace JobScheduler.Infrastructure.Services;

public class JobScheduleManagementService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IValidator<CreateJobScheduleRequest> createJobScheduleRequestValidator,
    IValidator<UpdateJobScheduleRequest> updateJobScheduleRequestValidator)
    : IJobScheduleManagementService
{
    public async Task<JobScheduleDto> CreateScheduleAsync(Guid jobId, CreateJobScheduleRequest request)
    {
        var validationResult = await createJobScheduleRequestValidator.ValidateAsync(request);

        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        var job = await unitOfWork.JobRepository.GetByIdAsync(jobId);
        if (job is null) throw new ValidationException("Job doesn't exist");

        var jobSchedule = new JobSchedule
        {
            JobId = jobId,
            Type = request.Type,
            CronExpression = request.CronExpression,
            TimeZone = request.TimeZone,
            IsActive = true
        };

        if (request.Type == JobScheduleType.Cron)
        {
            var cronExpression = CronExpression.Parse(request.CronExpression!);
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(request.TimeZone);
            jobSchedule.NextExecutionTime = cronExpression.GetNextOccurrence(DateTime.UtcNow, timeZoneInfo);
        }

        jobSchedule.StartAt = request.StartAt;
        jobSchedule.EndAt = request.EndAt;

        var createJobSchedule = await unitOfWork.JobScheduleRepository.AddAsync(jobSchedule);
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<JobScheduleDto>(createJobSchedule);
    }

    public async Task<JobScheduleDto> GetScheduleAsync(Guid scheduleId)
    {
        var schedule = await unitOfWork.JobScheduleRepository.GetByIdAsync(scheduleId);
        if (schedule is null) throw new ValidationException("Job schedule doesn't exist");

        return mapper.Map<JobScheduleDto>(schedule);
    }

    public async Task<IEnumerable<JobScheduleDto>> GetJobSchedulesAsync(Guid jobId)
    {
        var schedules = await unitOfWork.JobScheduleRepository.GetJobSchedulesAsync(jobId);
        return schedules.Select(mapper.Map<JobScheduleDto>);
    }

    public async Task UpdateScheduleAsync(Guid scheduleId, UpdateJobScheduleRequest request)
    {
        var schedule = await unitOfWork.JobScheduleRepository.GetByIdAsync(scheduleId);
        if (schedule is null) throw new ValidationException("Job schedule doesn't exist");

        schedule.Type = request.Type;
        schedule.CronExpression = request.CronExpression;
        schedule.StartAt = request.StartAt;
        schedule.EndAt = request.EndAt;
        schedule.TimeZone = request.TimeZone;
        schedule.NextExecutionTime = CalculateNextExecution(schedule);

        unitOfWork.JobScheduleRepository.Update(schedule);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteScheduleAsync(Guid scheduleId)
    {
        var schedule = await unitOfWork.JobScheduleRepository.GetByIdAsync(scheduleId);
        if (schedule is null) throw new ValidationException("Job schedule doesn't exist");

        await unitOfWork.JobScheduleRepository.DeleteAsync(scheduleId);
        await unitOfWork.SaveChangesAsync();
    }

    public DateTime? CalculateNextExecution(JobSchedule schedule)
    {
        if (schedule.Type == JobScheduleType.Cron
            && !string.IsNullOrEmpty(schedule.CronExpression))
        {
            var cronExpression = CronExpression.Parse(schedule.CronExpression);
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(schedule.TimeZone);
            return cronExpression.GetNextOccurrence(DateTime.UtcNow, timeZoneInfo);
        }

        throw new ValidationException("Job schedule doesn't have a valid cron expression or is not a cron type");
    }
}