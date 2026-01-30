using JobScheduler.Core.Models;

namespace JobScheduler.Core.Interfaces;

public interface IJobScheduleRepository : IRepository<JobSchedule>
{
    Task<IEnumerable<JobSchedule>> GetDueSchedulesAsync(DateTime currentTime);
    Task UpdateNextExecutionTimeAsync(Guid scheduleId, DateTime nextTime);
}