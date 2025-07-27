using Domain.Models;
using Repositories.Contracts.Base;

namespace Repositories.Contracts.RepositoryInterfaces;
public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<List<Category>> GetListAsync();
    //Task<Category?> GetByIdAsync(int id);
    //Task AddAsync(Category category);
    //Task UpdateAsync(Category category);
    //Task DeleteAsync(int id);
}

