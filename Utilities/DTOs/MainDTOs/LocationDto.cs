using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.MainDTOs;
public class LocationDto
{
    public string City { get; set; }
    public string Area { get; set; }
    public string Address { get; set; }
    public CoordinatesDto Coordinates { get; set; }
}
