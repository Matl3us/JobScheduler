using JobScheduler.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace JobScheduler.Infrastructure.Data;

public class JobSchedulerDbContext(DbContextOptions<JobSchedulerDbContext> options)
    : DbContext(options)
{
    public DbSet<Job> Jobs { get; set; }
    public DbSet<JobSchedule> Schedules { get; set; }
    public DbSet<JobExecution> Executions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Job>().ToTable("Jobs");

        modelBuilder.Entity<Job>()
            .HasMany(j => j.Schedules)
            .WithOne(s => s.Job)
            .HasForeignKey(s => s.JobId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Job>()
            .HasMany(j => j.Executions)
            .WithOne(e => e.Job)
            .HasForeignKey(e => e.JobId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Job>()
            .Property(j => j.Data)
            .HasColumnType("jsonb");
        modelBuilder.Entity<Job>()
            .Property(j => j.Version)
            .IsConcurrencyToken();
        modelBuilder.Entity<Job>()
            .Property(j => j.CreatedAt)
            .HasDefaultValueSql("now()");
        modelBuilder.Entity<Job>()
            .Property(j => j.IsActive)
            .HasDefaultValue(true);

        modelBuilder.Entity<Job>()
            .HasIndex(j => j.CreatedAt);
        modelBuilder.Entity<Job>()
            .HasIndex(j => j.IsActive);

        modelBuilder.Entity<JobSchedule>().ToTable("Schedules");

        modelBuilder.Entity<JobSchedule>()
            .HasMany(s => s.Executions)
            .WithOne(e => e.Schedule)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<JobSchedule>()
            .HasIndex(s => s.NextExecutionTime);
        modelBuilder.Entity<JobSchedule>()
            .HasIndex(s => s.IsActive);

        modelBuilder.Entity<JobExecution>().ToTable("Executions");

        modelBuilder.Entity<JobExecution>()
            .Property(e => e.Result)
            .HasColumnType("jsonb");
        modelBuilder.Entity<JobExecution>()
            .Property(e => e.RetryCount)
            .HasDefaultValue(0);

        modelBuilder.Entity<JobExecution>()
            .HasIndex(e => e.JobId);
        modelBuilder.Entity<JobExecution>()
            .HasIndex(e => e.Status);
        modelBuilder.Entity<JobExecution>()
            .HasIndex(e => e.StartedAt);
        modelBuilder.Entity<JobExecution>()
            .HasIndex(e => e.CompletedAt);
        modelBuilder.Entity<JobExecution>()
            .HasIndex(e => e.ScheduleId);
    }
}