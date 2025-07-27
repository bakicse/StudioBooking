using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.BaseModels;
public class BaseEntityKey
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Active")]
    public bool IsActive { get; set; }

    [NotMapped]
    public string? EncryptedId { get; set; }
}
