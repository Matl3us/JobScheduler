using System.ComponentModel.DataAnnotations;
using JobScheduler.Core.Enums;

namespace JobScheduler.Core.Models;

public class JobExecution
{
    public Guid Id { get; set; }
    
    [Required]
    public JobExecutionStatus Status { get; set; }
    
    public DateTime? StartedAt { get; set; }
    
    public DateTime? CompletedAt { get; set; }
    
    public long? ExecutionDurationMs { get; set; }
    
    public string? Result { get; set; }
    
    [MaxLength(2000)]
    public string? ErrorMessage { get; set; }
    
    [Range(0, 10)]
    public int RetryCount { get; set; }
    
    [MaxLength(100)]
    public string? WorkerId { get; set; }
    
    [Required]
    public Guid JobId { get; set; }
    public Job Job { get; set; }
    
    public Guid? ScheduleId { get; set; }
    public JobSchedule? Schedule { get; set; }
    
    public override string ToString()
    {
        var duration = ExecutionDurationMs.HasValue 
            ? $"{ExecutionDurationMs}ms" 
            : "N/A";
    
        return $"Execution [{Id}]: {Status} - Duration: {duration}";
    }
}