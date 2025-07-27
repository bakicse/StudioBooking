using AutoMapper;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts.Base;
using Serilog;
using Services.Concretes.ServiceInfrastructure;
using Services.Contracts.Base;
using Services.Contracts.ServiceInterfaces;
using Shared.Cryptography;

namespace Services.Concretes.Base;
public class ServiceManager(IRepositoryManager repository,
    IMapper mapper,
    IConfiguration configuration,
    ILogger logger) : IServiceManager
{
   private readonly Lazy<IBookingService> _bookingService= new(() => new BookingService(repository,configuration, mapper, logger));
    private readonly Lazy<IStudioService> _studioService= new(() => new StudioService(repository,configuration, mapper, logger));
    
    public IBookingService Booking => _bookingService.Value;
    public IStudioService Studio => _studioService.Value;
}
