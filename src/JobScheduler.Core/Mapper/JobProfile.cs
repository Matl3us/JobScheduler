using AutoMapper;
using JobScheduler.Core.DTOs;
using JobScheduler.Core.Models;

namespace JobScheduler.Core.Mapper;

public class JobProfile : Profile
{
    public JobProfile()
    {
        CreateMap<Job, JobDto>()
            .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
            .ForMember(d => d.Name, m => m.MapFrom(s => s.Name))
            .ForMember(d => d.Description, m => m.MapFrom(s => s.Description))
            .ForMember(d => d.CreatedAt, m => m.MapFrom(s => s.CreatedAt))
            .ForMember(d => d.IsActive, m => m.MapFrom(s => s.IsActive));
    }
}