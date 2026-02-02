using JobScheduler.Core.Interfaces;
using JobScheduler.Core.Models;
using JobScheduler.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobScheduler.Infrastructure.Repositories;

public class JobScheduleRepository(JobSchedulerDbContext dbContext)
    : GenericRepository<JobSchedule>(dbContext), IJobScheduleRepository
{
    public async Task<IEnumerable<JobSchedule>> GetJobSchedulesAsync(Guid jobId)
    {
        return await DbSet.Where(s => s.JobId == jobId)
            .ToListAsync();
    }

    public async Task<IEnumerable<JobSchedule>> GetDueSchedulesAsync(DateTime currentTime)
    {
        return await DbSet.Where(s =>
                s.IsActive && s.NextExecutionTime <= currentTime && s.Job.IsActive)
            .ToListAsync();
    }

    public async Task UpdateNextExecutionTimeAsync(Guid scheduleId, DateTime nextTime)
    {
        await DbSet.Where(s => s.Id == scheduleId)
            .ExecuteUpdateAsync(s =>
                s.SetProperty(sc => sc.NextExecutionTime, nextTime));
    }
}