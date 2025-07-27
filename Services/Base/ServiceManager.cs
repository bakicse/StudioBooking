using AutoMapper;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts.Base;
using Serilog;
using Services.Concretes.ServiceInfrastructure;
using Services.Contracts.Base;
using Services.Contracts.ServiceInterfaces;
using Shared.Cryptography;

namespace Services.Concretes.Base;
public class ServiceManager(IRepositoryManager repository,
    IMapper mapper,
    IConfiguration configuration,
    ILogger logger) : IServiceManager
{
    private readonly Lazy<ICategoryService> _categoryService = new(() => new CategoryService(repository,configuration, mapper, logger));
    private readonly Lazy<ISubCategoryService> _subCategoryService= new(() => new SubCategoryService(repository,configuration, mapper, logger));
    public ICategoryService Category => _categoryService.Value;
    public ISubCategoryService SubCategory => _subCategoryService.Value;
}
