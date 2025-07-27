namespace Repositories.Contracts.Base;

public interface IBaseRepository<T> 
    where T : class
{
    Task<IQueryable<T>> AllAsync();
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> FindByIdAsync(int id);
    Task<T?> GetInsertedObjAsync(T entity);
    Task<bool> InsertAsync(T entity);
    Task<bool> InsertRangeAsync(IEnumerable<T> entities);
    Task<bool> UpdateAsync(T entity);
    Task<bool> UpdateRangeAsync(IEnumerable<T> entities);  //Need to be tested & to be fixed the implementation.
    Task<bool> DeleteAsync(T entity);
    Task<bool> DeleteRangeAsync(IEnumerable<T> entities);
}
