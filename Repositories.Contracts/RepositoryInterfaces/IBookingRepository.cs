using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts.RepositoryInterfaces;
public interface IBookingRepository
{
    Task<Booking> CreateBookingAsync(Booking booking);
    Task<IEnumerable<Booking>> GetAllBookingsAsync();
    Task<IEnumerable<Booking>> GetBookingsForStudioAndDateAsync(int studioId, DateTime date);
}
