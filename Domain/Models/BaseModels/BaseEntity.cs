using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.BaseModels;
public class BaseEntity : BaseEntityKey
{
    [Display(Name = "Created Date")]
    public DateTime? CreatedDate { get; set; }

    [Display(Name = "Modified Date")]
    public DateTime? UpdatedDate { get; set; }

    [Display(Name = "Author")]
    public int? CreatedBy { get; set; }

    [Display(Name = "Modifier")]
    public int? UpdatedBy { get; set; }


    [NotMapped]
    public string? Creator { get; set; }

    [NotMapped]
    public string? Modifier { get; set; }
}
