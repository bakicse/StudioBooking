using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.BaseDTOs;
public class BaseDtoTempId : BaseDto
{
    [Key]
    public int TempId { get; set; }
}
