namespace JobScheduler.Core.DTOs;

public class HttpRequestDto
{
    public required string Uri { get; init; }
    public required string Method { get; init; }
    public Dictionary<string, string> Headers { get; init; } = new();
    public string? Body { get; init; }
}