using Domain.Models;

namespace Repositories.Contracts.RepositoryInterfaces;
public interface IBookingRepository
{
    Task<Booking> CreateBookingAsync(Booking booking);
    Task<IEnumerable<Booking>> GetAllBookingsAsync();
    Task<IEnumerable<Booking>> GetBookingsForStudioAndDateAsync(int studioId, DateTime date);
}
