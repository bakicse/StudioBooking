using Repositories.Contracts.RepositoryInterfaces;

namespace Repositories.Contracts.Base;
public interface IRepositoryManager
{
    public ICategoryRepository Category { get; }
    public ISubCategoryRepository SubCategory { get; }
}
