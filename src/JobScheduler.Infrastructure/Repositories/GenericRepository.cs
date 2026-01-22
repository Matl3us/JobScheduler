using JobScheduler.Core.Interfaces;
using JobScheduler.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobScheduler.Infrastructure.Repositories;

public class GenericRepository<T>(JobSchedulerDbContext dbContext) : IRepository<T>
    where T : class, IEntity
{
    protected readonly JobSchedulerDbContext DbContext = dbContext;
    protected readonly DbSet<T> DbSet = dbContext.Set<T>();

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        var entry = await DbSet.AddAsync(entity);
        return entry.Entity;
    }

    public void Update(T entity)
    {
        DbSet.Attach(entity);
        DbContext.Entry(entity).State = EntityState.Modified;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entry = await DbSet.FindAsync(id);
        if (entry is not null)
            DbSet.Remove(entry);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await DbSet.FindAsync(id) is not null;
    }
}