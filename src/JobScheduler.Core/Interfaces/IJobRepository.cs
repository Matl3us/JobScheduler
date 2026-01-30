using JobScheduler.Core.Models;

namespace JobScheduler.Core.Interfaces;

public interface IJobRepository : IRepository<Job>
{
    Task<IEnumerable<Job>> GetActiveJobsAsync();
    Task<Job?> GetJobWithSchedulesAsync(Guid jobId);
}