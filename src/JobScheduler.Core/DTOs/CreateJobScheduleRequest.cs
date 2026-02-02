using JobScheduler.Core.Enums;

namespace JobScheduler.Core.DTOs;

public class CreateJobScheduleRequest
{
    public Guid JobId { get; init; }
    public JobScheduleType Type { get; init; }
    public string? CronExpression { get; init; }
    public DateTime? StartAt { get; init; }
    public DateTime? EndAt { get; init; }
    public string TimeZone { get; init; } = "";
}