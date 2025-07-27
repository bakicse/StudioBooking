using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.MainDTOs;
public class SubCategoryDto
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    [Display(Name = "Sub-Category Name")]
    public string SubCategoryName { get; set; } = null!;

    [Required]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    public bool IsActive { get; set; }
}
