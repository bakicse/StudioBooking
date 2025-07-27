using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.BaseDTOs;
public class BaseViewModel : BaseDto
{
    [Display(Name = "Author")]
    public string Creator { get; set; } = null!;

    [Display(Name = "Modifier")] 
    public string Modifier { get; set; } = null!;

    [Display(Name = "Created Date")]
    public DateTime? CreatedDate { get; set; }

    [Display(Name = "Modified Date")]
    public DateTime? UpdatedDate { get; set; }
}
