using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.BaseDTOs;
public class BaseDtoId : BaseDto
{
    [Key]
    public int Id { get; set; }
}
