using Domain.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts.RepositoryInterfaces;

namespace Repositories.Concretes.RepositoryInfrastructure;
public class BookingRepository(AppDbContext context) : IBookingRepository
{
    public async Task<Booking> CreateBookingAsync(Booking booking)
    {
        context.Bookings.Add(booking);
        await context.SaveChangesAsync();
        return booking;
    }

    public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
    {
        return await context.Bookings
            .Include(b => b.Studio) // Include studio details if needed for display
            .ToListAsync();
    }

    public async Task<IEnumerable<Booking>> GetBookingsForStudioAndDateAsync(int studioId, DateTime date)
    {
        // We compare only the date part
        return await context.Bookings
            .Where(b => b.StudioId == studioId &&
                        b.BookingDate.Equals(date.Date))
            .ToListAsync();
    }
}
