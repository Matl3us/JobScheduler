using AutoMapper;
using JobScheduler.Core.DTOs;
using JobScheduler.Core.Models;

namespace JobScheduler.Core.Mapper;

public class JobScheduleProfile : Profile
{
    public JobScheduleProfile()
    {
        CreateMap<JobSchedule, JobScheduleDto>()
            .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
            .ForMember(d => d.Type, m => m.MapFrom(s => s.Type))
            .ForMember(d => d.CronExpression, m => m.MapFrom(s => s.CronExpression))
            .ForMember(d => d.StartAt, m => m.MapFrom(s => s.StartAt))
            .ForMember(d => d.EndAt, m => m.MapFrom(s => s.EndAt))
            .ForMember(d => d.TimeZone, m => m.MapFrom(s => s.TimeZone))
            .ForMember(d => d.IsActive, m => m.MapFrom(s => s.IsActive))
            .ForMember(d => d.NextExecutionTime, m => m.MapFrom(s => s.NextExecutionTime))
            .ForMember(d => d.LastExecutionTime, m => m.MapFrom(s => s.LastExecutionTime));
    }
}