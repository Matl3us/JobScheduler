using JobScheduler.Core.Interfaces;
using JobScheduler.Core.Models;
using JobScheduler.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobScheduler.Infrastructure.Repositories;

public class JobExecutionRepository(JobSchedulerDbContext dbContext)
    : GenericRepository<JobExecution>(dbContext), IJobExecutionRepository
{
    public async Task<IEnumerable<JobExecution>> GetExecutionHistoryAsync(Guid jobId, int pageSize, int page)
    {
        return await DbSet.Where(e => e.JobId == jobId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<JobExecution> GetLatestExecutionAsync(Guid jobId)
    {
        return await DbSet.Where(e => e.JobId == jobId)
            .OrderBy(e => e.CompletedAt)
            .FirstAsync();
    }
}