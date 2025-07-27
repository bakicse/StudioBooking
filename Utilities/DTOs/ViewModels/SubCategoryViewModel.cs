using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.ViewModels;
public class SubCategoryViewModel
{
    [Display(Name = "Category Name")]
    public string? CategoryName { get; set; }
    [Display(Name = "Sub-Category Name")]
    public string? SubCategoryName { get; set; }
    public bool IsActive { get; set; }
}
