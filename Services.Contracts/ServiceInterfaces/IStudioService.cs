using Shared.DTOs.MainDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.ServiceInterfaces;
public interface IStudioService
{
    Task<IEnumerable<StudioDto>> GetAllStudiosAsync();
    Task<StudioDto> GetStudioByIdAsync(int id);
    Task<IEnumerable<StudioDto>> SearchStudiosByAreaAsync(string area);
    Task<IEnumerable<StudioDto>> SearchStudiosNearbyAsync(double lat, double lng, double radiusKm);
}
