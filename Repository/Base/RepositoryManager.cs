using Domain.Data;
using Repositories.Concretes.RepositoryInfrastructure;
using Repositories.Contracts.Base;
using Repositories.Contracts.RepositoryInterfaces;
using Shared.Cryptography;

namespace Repositories.Concretes.Base;
public sealed class RepositoryManager(AppDbContext context) : IRepositoryManager
{

    private readonly Lazy<ICategoryRepository> _category = new(() => new CategoryRepository(context));
    private readonly Lazy<ISubCategoryRepository> _subCategory = new(() => new SubCategoryRepository(context));

    public ICategoryRepository Category => _category.Value;
    public ISubCategoryRepository SubCategory => _subCategory.Value;
}
