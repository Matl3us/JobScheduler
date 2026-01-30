using JobScheduler.Core.Enums;

namespace JobScheduler.Core.DTOs;

public class CreateJobRequest
{
    public required string Name { get; init; }
    public JobType Type { get; init; }
    public string Description { get; init; } = "";
    public required string Data { get; init; }
    public required string CreatedBy { get; init; }
}