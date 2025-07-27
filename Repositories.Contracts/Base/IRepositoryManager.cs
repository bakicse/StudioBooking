using Repositories.Contracts.RepositoryInterfaces;

namespace Repositories.Contracts.Base;
public interface IRepositoryManager
{
    public IStudioRepository Studio { get; }
    public IBookingRepository Booking { get; }
}
