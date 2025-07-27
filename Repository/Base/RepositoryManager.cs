using Domain.Data;
using Repositories.Concretes.RepositoryInfrastructure;
using Repositories.Contracts.Base;
using Repositories.Contracts.RepositoryInterfaces;
using Shared.Cryptography;

namespace Repositories.Concretes.Base;
public sealed class RepositoryManager(AppDbContext context) : IRepositoryManager
{

    private readonly Lazy<IBookingRepository> _booking = new(() => new BookingRepository(context));
    private readonly Lazy<IStudioRepository> _studio = new(() => new StudioRepository(context));

    public IBookingRepository Booking => _booking.Value;
    public IStudioRepository Studio => _studio.Value;
}
