using JobScheduler.Core.DTOs;

namespace JobScheduler.Core.Interfaces;

public interface IJobManagementService
{
    Task<JobDto> CreateJobAsync(CreateJobRequest request);
    Task<JobDto> GetJobAsync(Guid jobId);
    Task<IEnumerable<JobDto>> GetAllJobsAsync();
    Task UpdateJobAsync(Guid jobId, UpdateJobRequest request);
    Task DeleteJobAsync(Guid jobId);
    Task ActivateJobAsync(Guid jobId);
    Task DeactivateJobAsync(Guid jobId);
}