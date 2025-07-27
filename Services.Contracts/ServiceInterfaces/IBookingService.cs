using Shared.DTOs.MainDTOs;

namespace Services.Contracts.ServiceInterfaces;
public interface IBookingService
{
    Task<BookingDto> CreateBookingAsync(CreateBookingRequestDto request);
    Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
    Task<IEnumerable<AvailableTimeSlotDto>> GetAvailableTimeSlotsAsync(int studioId, DateTime date);
}
