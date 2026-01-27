using JobScheduler.Core.Interfaces;
using JobScheduler.Infrastructure.Data;

namespace JobScheduler.Infrastructure.Repositories;

public class UnitOfWork(JobSchedulerDbContext dbContext) : IUnitOfWork, IDisposable, IAsyncDisposable
{
    public async ValueTask DisposeAsync()
    {
        await dbContext.DisposeAsync();
    }

    public void Dispose()
    {
        dbContext.Dispose();
    }

    public IJobRepository JobRepository { get; } = new JobRepository(dbContext);
    public IJobScheduleRepository JobScheduleRepository { get; } = new JobScheduleRepository(dbContext);
    public IJobExecutionRepository JobExecutionRepository { get; } = new JobExecutionRepository(dbContext);

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}