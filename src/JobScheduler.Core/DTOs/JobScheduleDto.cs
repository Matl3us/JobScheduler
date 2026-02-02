using JobScheduler.Core.Enums;

namespace JobScheduler.Core.DTOs;

public class JobScheduleDto
{
    public Guid Id { get; init; }
    public JobScheduleType Type { get; init; }
    public string? CronExpression { get; init; }
    public DateTime? StartAt { get; init; }
    public DateTime? EndAt { get; init; }
    public string TimeZone { get; init; } = "";
    public bool IsActive { get; init; }
    public DateTime? NextExecutionTime { get; init; }
    public DateTime? LastExecutionTime { get; init; }
}