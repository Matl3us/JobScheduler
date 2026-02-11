using JobScheduler.Core.DTOs;
using JobScheduler.Core.Models;

namespace JobScheduler.Core.Interfaces;

public interface IJobScheduleManagementService
{
    Task<JobScheduleDto> CreateScheduleAsync(Guid jobId, CreateJobScheduleRequest request);
    Task<JobScheduleDto> GetScheduleAsync(Guid scheduleId);
    Task<IEnumerable<JobScheduleDto>> GetJobSchedulesAsync(Guid jobId);
    Task UpdateScheduleAsync(Guid scheduleId, UpdateJobScheduleRequest request);
    Task DeleteScheduleAsync(Guid scheduleId);
    DateTime? CalculateNextExecution(JobSchedule schedule);
}