using Services.Contracts.ServiceInterfaces;

namespace Services.Contracts.Base;
public interface IServiceManager
{
    public ICategoryService Category { get; }
    public ISubCategoryService SubCategory { get; }
}
