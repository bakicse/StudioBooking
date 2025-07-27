using Domain.Models;
using Repositories.Contracts.Base;

namespace Repositories.Contracts.RepositoryInterfaces;
public interface ISubCategoryRepository : IBaseRepository<SubCategory>
{
    Task<List<SubCategory>> GetListAsync();
    Task<(List<SubCategory>, int)> GetPagedAsync(int page, int pageSize);
    //Task<SubCategory?> GetByIdAsync(int id);
    //Task AddAsync(SubCategory entity);
    //Task UpdateAsync(SubCategory entity);
    //Task DeleteAsync(int id);
}

