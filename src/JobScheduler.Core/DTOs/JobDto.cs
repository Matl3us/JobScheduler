namespace JobScheduler.Core.DTOs;

public class JobDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public string Description { get; init; } = "";
    public DateTime CreatedAt { get; init; }
    public bool IsActive { get; init; }
}