using JobScheduler.Core.DTOs;

namespace JobScheduler.Core.Interfaces;

public interface IJobExecutionManagementService
{
    Task<IEnumerable<JobExecutionDto>> GetJobExecutionsAsync(Guid jobId, int pageSize, int page);
    Task<JobExecutionDto> GetExecutionAsync(Guid executionId);
}