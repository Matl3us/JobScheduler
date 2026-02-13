namespace JobScheduler.Core.DTOs;

public class JobExecutionDto
{
    public Guid Id { get; init; }
    public required string Status { get; init; }
    public DateTime StartedAt { get; init; }
    public DateTime CompletedAt { get; init; }
    public long? ExecutionDurationMs { get; init; }
    public string? Result { get; init; }
    public string? ErrorMessage { get; init; }
    public int RetryCount { get; init; }
}