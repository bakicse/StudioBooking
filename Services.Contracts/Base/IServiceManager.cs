using Services.Contracts.ServiceInterfaces;

namespace Services.Contracts.Base;
public interface IServiceManager
{
    public IStudioService Studio { get; }
    public IBookingService Booking { get; }
}
