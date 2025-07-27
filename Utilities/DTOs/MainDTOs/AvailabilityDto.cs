using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.MainDTOs;
public class AvailabilityDto
{
    public TimeOnly OpenTime { get; set; }
    public TimeOnly CloseTime { get; set; }
}
