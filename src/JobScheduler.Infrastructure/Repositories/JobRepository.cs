using JobScheduler.Core.Interfaces;
using JobScheduler.Core.Models;
using JobScheduler.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobScheduler.Infrastructure.Repositories;

public class JobRepository(JobSchedulerDbContext dbContext)
    : GenericRepository<Job>(dbContext), IJobRepository
{
    public async Task<IEnumerable<Job>> GetActiveJobsAsync()
    {
        return await DbSet.Where(j => j.IsActive)
            .ToListAsync();
    }

    public async Task<Job> GetJobWithSchedulesAsync(Guid jobId)
    {
        return await DbSet.Where(j => j.Id == jobId)
            .Include(j => j.Schedules)
            .FirstAsync();
    }
}