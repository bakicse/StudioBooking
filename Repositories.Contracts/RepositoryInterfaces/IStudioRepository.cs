using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts.RepositoryInterfaces;
public interface IStudioRepository
{
    Task<IEnumerable<Studio>> GetAllStudiosAsync();
    Task<Studio> GetStudioByIdAsync(int id);
    Task<IEnumerable<Studio>> SearchStudiosByAreaAsync(string area);
    Task<IEnumerable<Studio>> SearchStudiosNearbyAsync(double lat, double lng, double radiusKm);
}
