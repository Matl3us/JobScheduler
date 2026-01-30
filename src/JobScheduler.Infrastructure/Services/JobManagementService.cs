using AutoMapper;
using FluentValidation;
using JobScheduler.Core.DTOs;
using JobScheduler.Core.Interfaces;
using JobScheduler.Core.Models;

namespace JobScheduler.Infrastructure.Services;

public class JobManagementService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IValidator<CreateJobRequest> createJobRequestValidator,
    IValidator<UpdateJobRequest> updateJobRequestValidator)
    : IJobManagementService
{
    public async Task<JobDto> CreateJobAsync(CreateJobRequest request)
    {
        var validationResult = await createJobRequestValidator.ValidateAsync(request);

        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        var job = new Job
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Type = request.Type,
            Data = request.Data,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = request.CreatedBy,
            IsActive = true,
            Version = 1
        };

        var createdJob = await unitOfWork.JobRepository.AddAsync(job);
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<JobDto>(createdJob);
    }

    public async Task<JobDto> GetJobAsync(Guid jobId)
    {
        var job = await unitOfWork.JobRepository.GetByIdAsync(jobId);
        return mapper.Map<JobDto>(job);
    }

    public async Task<IEnumerable<JobDto>> GetAllJobsAsync()
    {
        var jobs = await unitOfWork.JobRepository.GetAllAsync();
        return jobs.Select(mapper.Map<JobDto>);
    }

    public async Task UpdateJobAsync(Guid jobId, UpdateJobRequest request)
    {
        var validationResult = await updateJobRequestValidator.ValidateAsync(request);

        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        var job = new Job
        {
            Name = request.Name,
            Type = request.Type,
            Description = request.Description,
            Data = request.Data
        };

        unitOfWork.JobRepository.Update(job);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteJobAsync(Guid jobId)
    {
        await unitOfWork.JobRepository.DeleteAsync(jobId);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task ActivateJobAsync(Guid jobId)
    {
        var job = await unitOfWork.JobRepository.GetByIdAsync(jobId);

        if (job is not null)
        {
            job.IsActive = true;
            await unitOfWork.SaveChangesAsync();
        }
    }

    public async Task DeactivateJobAsync(Guid jobId)
    {
        var job = await unitOfWork.JobRepository.GetByIdAsync(jobId);

        if (job is not null)
        {
            job.IsActive = false;
            await unitOfWork.SaveChangesAsync();
        }
    }
}