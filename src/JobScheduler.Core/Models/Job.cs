using System.ComponentModel.DataAnnotations;
using JobScheduler.Core.Enums;

namespace JobScheduler.Core.Models;

public class Job
{
   public Guid Id { get; set; } 
   
   [Required]
   [MaxLength(200)]
   public string Name { get; set; }
   
   [MaxLength(1000)]
   public string Description { get; set; }
   
   [Required]
   public JobType Type { get; set; }
   
   [Required]
   public string Data { get; set; }
   
   public DateTime CreatedAt { get; set; }
   
   [Required]
   [MaxLength(100)]
   public string CreatedBy { get; set; }
   
   public bool IsActive { get; set; }
   
   [ConcurrencyCheck]
   public int Version { get; set; }
   
   public ICollection<JobSchedule> Schedules { get; set; } = [];
   public ICollection<JobExecution> Executions { get; set; } = [];

   public override string ToString()
   {
       return $"Job [{Id}]: {Name} ({Type}) - Active: {IsActive}";
   }
}