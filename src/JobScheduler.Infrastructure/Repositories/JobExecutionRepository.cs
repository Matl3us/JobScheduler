using JobScheduler.Core.Interfaces;
using JobScheduler.Core.Models;
using JobScheduler.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobScheduler.Infrastructure.Repositories;

public class JobExecutionRepository(JobSchedulerDbContext dbContext)
    : GenericRepository<JobExecution>(dbContext), IJobExecutionRepository
{
    public async Task<IEnumerable<JobExecution>> GetPaginatedJobExecutionsAsync(Guid jobId, int pageSize = 25,
        int page = 1)
    {
        return await DbSet.Where(e => e.JobId == jobId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .OrderByDescending(e => e.CompletedAt)
            .ToListAsync();
    }

    public async Task<JobExecution?> GetExecutionAsync(Guid executionId)
    {
        return await DbSet.FirstOrDefaultAsync(e => e.Id == executionId);
    }
}