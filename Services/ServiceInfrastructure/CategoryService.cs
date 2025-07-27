using AutoMapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts.Base;
using Services.Contracts.ServiceInterfaces;
using Shared.DTOs.MainDTOs;
using Shared.DTOs.ViewModels;

namespace Services.Concretes.ServiceInfrastructure;
public class CategoryService(
    IRepositoryManager repository,
    IConfiguration configuration,
    IMapper mapper,
    Serilog.ILogger logger)
    : ICategoryService
{

    public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
    {
        var categories = await repository.Category.GetListAsync();
        return mapper.Map<IEnumerable<CategoryViewModel>>(categories);
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await repository.Category.FindByIdAsync(id);
        return mapper.Map<CategoryDto?>(category);
    }

    public async Task<bool> CreateAsync(CategoryDto categoryDto)
    {
        var category = mapper.Map<Category>(categoryDto);

        return await repository.Category.InsertAsync(category);
    }
    public async Task<bool> UpdateAsync(CategoryDto categoryDto)
    {
        var category = mapper.Map<Category>(categoryDto);

        var existingCategory = await repository.Category.FindByIdAsync(categoryDto.Id);
        if (existingCategory is null)
            return default;

        category.Id = existingCategory.Id;
        return await repository.Category.UpdateAsync(category);
    }
    public async Task<bool> DeleteAsync(int id)
    {

        var category = await repository.Category.FindByIdAsync(id);
        if (category is null)
            return default;
        category.IsDeleted = true; // Soft delete
        return await repository.Category.UpdateAsync(category);
    }
}