using JobScheduler.Core.Models;

namespace JobScheduler.Core.Interfaces;

public interface IJobRepository
{
    Task<IEnumerable<Job>> GetActiveJobsAsync();
    Task<Job?> GetJobWithSchedulesAsync(Guid jobId);
}