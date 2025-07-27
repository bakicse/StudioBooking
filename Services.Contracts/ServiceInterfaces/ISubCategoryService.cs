using Shared.DTOs.MainDTOs;
using Shared.DTOs.ViewModels;

namespace Services.Contracts.ServiceInterfaces;
public interface ISubCategoryService
{
    Task<(IEnumerable<SubCategoryViewModel>, int)> GetPagedAsync(int page, int pageSize);
    Task<IEnumerable<SubCategoryViewModel>> GetAllAsync();
    Task<SubCategoryDto?> GetByIdAsync(int id);
    Task<bool> CreateAsync(SubCategoryDto subCategoryDto);
    Task<bool> UpdateAsync(SubCategoryDto subCategoryDto);
    Task<bool> DeleteAsync(int id);
}

