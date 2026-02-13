using JobScheduler.Core.Models;

namespace JobScheduler.Core.Interfaces;

public interface IJobExecutionRepository : IRepository<JobExecution>
{
    Task<IEnumerable<JobExecution>> GetPaginatedJobExecutionsAsync(Guid jobId, int pageSize, int page);
    Task<JobExecution?> GetExecutionAsync(Guid executionId);
}