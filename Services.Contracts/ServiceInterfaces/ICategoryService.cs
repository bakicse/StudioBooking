using Shared.DTOs.MainDTOs;
using Shared.DTOs.ViewModels;

namespace Services.Contracts.ServiceInterfaces;
public interface ICategoryService
{
    Task<IEnumerable<CategoryViewModel>> GetAllAsync();
    Task<CategoryDto?> GetByIdAsync(int id);
    Task<bool> CreateAsync(CategoryDto categoryDto);
    Task<bool> UpdateAsync(CategoryDto categoryDto);
    Task<bool> DeleteAsync(int id);
}
