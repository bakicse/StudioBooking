using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.BaseDTOs;
public class BaseDto
{
    [Display(Name = "Active")]
    public bool IsActive { get; set; }

    public string? EncryptedId { get; set; }
}
