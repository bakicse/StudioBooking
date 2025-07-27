using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.MainDTOs;
public class CreateBookingRequestDto
{
    public int StudioId { get; set; }
    public DateTime BookingDate { get; set; } // Date only, e.g., "YYYY-MM-DD"
    public string TimeSlot { get; set; }      // e.g., "09:00-10:00"
    public string UserName { get; set; }      // Matches SQL column name
    public string Email { get; set; }
}
