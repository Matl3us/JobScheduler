using JobScheduler.Core.Models;

namespace JobScheduler.Core.Interfaces;

public interface IJobExecutionRepository : IRepository<JobExecution>
{
    Task<IEnumerable<JobExecution>> GetExecutionHistoryAsync(Guid jobId, int pageSize, int page);
    Task<JobExecution?> GetLatestExecutionAsync(Guid jobId);
}