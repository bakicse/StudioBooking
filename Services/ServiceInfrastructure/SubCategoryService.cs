using AutoMapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts.Base;
using Services.Contracts.ServiceInterfaces;
using Shared.DTOs.MainDTOs;
using Shared.DTOs.ViewModels;

namespace Services.Concretes.ServiceInfrastructure;
public class SubCategoryService(IRepositoryManager repository,
    IConfiguration configuration,
    IMapper mapper,
    Serilog.ILogger logger) : ISubCategoryService
{
    public async Task<(IEnumerable<SubCategoryViewModel>, int)> GetPagedAsync(int page, int pageSize)
    {
        var (subcategories, totalCount) = await repository.SubCategory.GetPagedAsync(page, pageSize);

        var mappedViewModels = mapper.Map<IEnumerable<SubCategoryViewModel>>(subcategories);
        return (mappedViewModels, totalCount);
    }


    public async Task<IEnumerable<SubCategoryViewModel>> GetAllAsync()
    {
        var subCategories = await repository.SubCategory.GetListAsync();
        return mapper.Map<IEnumerable<SubCategoryViewModel>>(subCategories);
    }

    public async Task<SubCategoryDto?> GetByIdAsync(int id)
    {
        var subCategory = await repository.SubCategory.FindByIdAsync(id);
        return mapper.Map<SubCategoryDto?>(subCategory);
    }

    public async Task<bool> CreateAsync(SubCategoryDto subCategoryDto)
    {
        var subCategory = mapper.Map<SubCategory>(subCategoryDto);

        return await repository.SubCategory.InsertAsync(subCategory);
    }
    public async Task<bool> UpdateAsync(SubCategoryDto subCategoryDto)
    {
        var subCategory = mapper.Map<SubCategory>(subCategoryDto);

        var existingCategory = await repository.SubCategory.FindByIdAsync(subCategoryDto.Id);
        if (existingCategory is null)
            return default;

        subCategory.Id = existingCategory.Id;
        return await repository.SubCategory.UpdateAsync(subCategory);
    }
    public async Task<bool> DeleteAsync(int id)
    {

        var subCategory = await repository.SubCategory.FindByIdAsync(id);
        if (subCategory is null)
            return default;
        subCategory.IsDeleted = true; // Soft delete
        return await repository.SubCategory.UpdateAsync(subCategory);
    }
}

