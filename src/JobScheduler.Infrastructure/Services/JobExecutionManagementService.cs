using AutoMapper;
using JobScheduler.Core.DTOs;
using JobScheduler.Core.Interfaces;

namespace JobScheduler.Infrastructure.Services;

public class JobExecutionManagementService(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IJobExecutionManagementService
{
    public async Task<IEnumerable<JobExecutionDto>> GetJobExecutionsAsync(Guid jobId, int pageSize = 25, int page = 1)
    {
        var executions = await unitOfWork.JobExecutionRepository
            .GetPaginatedJobExecutionsAsync(jobId, pageSize, page);
        return executions.Select(mapper.Map<JobExecutionDto>);
    }

    public async Task<JobExecutionDto> GetExecutionAsync(Guid executionId)
    {
        var execution = await unitOfWork.JobExecutionRepository
            .GetExecutionAsync(executionId);
        return mapper.Map<JobExecutionDto>(execution);
    }
}