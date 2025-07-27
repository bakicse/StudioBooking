using Shared.DTOs.MainDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.ServiceInterfaces;
public interface IBookingService
{
    Task<BookingDto> CreateBookingAsync(CreateBookingRequestDto request);
    Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
    Task<IEnumerable<AvailableTimeSlotDto>> GetAvailableTimeSlotsAsync(int studioId, DateTime date);
}
