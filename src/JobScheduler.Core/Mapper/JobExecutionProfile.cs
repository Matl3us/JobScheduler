using AutoMapper;
using JobScheduler.Core.DTOs;
using JobScheduler.Core.Models;

namespace JobScheduler.Core.Mapper;

public class JobExecutionProfile : Profile
{
    public JobExecutionProfile()
    {
        CreateMap<JobExecution, JobExecutionDto>()
            .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
            .ForMember(d => d.Status, m => m.MapFrom(s => s.Status))
            .ForMember(d => d.StartedAt, m => m.MapFrom(s => s.StartedAt))
            .ForMember(d => d.CompletedAt, m => m.MapFrom(s => s.CompletedAt))
            .ForMember(d => d.ExecutionDurationMs, m => m.MapFrom(s => s.ExecutionDurationMs))
            .ForMember(d => d.Result, m => m.MapFrom(s => s.Result))
            .ForMember(d => d.ErrorMessage, m => m.MapFrom(s => s.ErrorMessage))
            .ForMember(d => d.RetryCount, m => m.MapFrom(s => s.RetryCount));
    }
}