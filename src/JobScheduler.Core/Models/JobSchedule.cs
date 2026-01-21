using System.ComponentModel.DataAnnotations;
using JobScheduler.Core.Enums;

namespace JobScheduler.Core.Models;

public class JobSchedule
{
    public Guid Id { get; set; }
    
    [Required]
    public JobScheduleType Type { get; set; }
    
    [MaxLength(100)]
    public string? CronExpression { get; set; }
    
    public DateTime? StartAt { get; set; }
    
    public DateTime? EndAt { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string TimeZone { get; set; }
    
    public bool IsActive { get; set; }
    
    public DateTime? NextExecutionTime { get; set; }
    
    public DateTime? LastExecutionTime { get; set; }
    
    [Required]
    public Guid JobId { get; set; }
    public Job Job { get; set; }
    
    public ICollection<JobExecution> Executions { get; set; } = [];
    
    public override string ToString()
    {
        var scheduleInfo = Type switch
        {
            JobScheduleType.Cron => $"Cron: {CronExpression}",
            JobScheduleType.OneTime => $"OneTime: {StartAt:yyyy-MM-dd HH:mm}",
            JobScheduleType.Recurring => $"Recurring",
            _ => Type.ToString()
        };
    
        return $"Schedule [{Id}]: {scheduleInfo} - Active: {IsActive}, Next: {NextExecutionTime:yyyy-MM-dd HH:mm}";
    }
}
