namespace JobScheduler.Core.Interfaces;

public interface IUnitOfWork
{
    IJobRepository JobRepository { get; }
    IJobScheduleRepository JobScheduleRepository { get; }
    IJobExecutionRepository JobExecutionRepository { get; }

    Task SaveChangesAsync();
}