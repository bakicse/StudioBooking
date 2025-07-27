using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts.Base;

namespace Repositories.Concretes.Base;

public class BaseRepository<T>(AppDbContext context) : IBaseRepository<T> 
    where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<IQueryable<T>> AllAsync()
    {
        return (await _dbSet.AsNoTracking().ToListAsync()).AsQueryable();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteRangeAsync(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<T?> FindByIdAsync(int id)
    {
        context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        return await _dbSet.FindAsync(id);
    }

    public async Task<T?> GetInsertedObjAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return await context.SaveChangesAsync() > 0 ? entity : null;
    }

    public async Task<bool> InsertAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> InsertRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        var result = _dbSet.Attach(entity);
        result.State = EntityState.Modified;
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateRangeAsync(IEnumerable<T> entities)
    {
        _dbSet.UpdateRange(entities);
        return await context.SaveChangesAsync() > 0;
    }
}
