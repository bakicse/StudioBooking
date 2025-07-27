using Domain.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts.RepositoryInterfaces;

namespace Repositories.Concretes.RepositoryInfrastructure;
public class StudioRepository (AppDbContext context) : IStudioRepository
{

    public async Task<IEnumerable<Studio>> GetAllStudiosAsync()
    {
        return await context.Studios
                             .Include(s => s.Location)
                                 .ThenInclude(l => l.Coordinates)
                             .Include(s => s.Contact)
                             .Include(s => s.Availability)
                             .ToListAsync();
    }

    public async Task<Studio> GetStudioByIdAsync(int id)
    {
        return await context.Studios
                             .Include(s => s.Location)
                                 .ThenInclude(l => l.Coordinates)
                             .Include(s => s.Contact)
                             .Include(s => s.Availability)
                             .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Studio>> SearchStudiosByAreaAsync(string area)
    {
        return await context.Studios
                             .Include(s => s.Location)
                                 .ThenInclude(l => l.Coordinates)
                             .Include(s => s.Contact)
                             .Include(s => s.Availability)
                             .Where(s => s.Location.Area.Contains(area))
                             .ToListAsync();
    }

    public async Task<IEnumerable<Studio>> SearchStudiosNearbyAsync(double lat, double lng, double radiusKm)
    {
        // This is a simplified Haversine formula implementation for demonstration.
        // For production, consider using a spatial library (e.g., NetTopologySuite with EF Core)
        // or directly using SQL Server's spatial types (Geography data type).

        // Earth's radius in kilometers
        const double R = 6371;

        var nearbyStudios = await context.Studios
            .Include(s => s.Location)
                .ThenInclude(l => l.Coordinates)
            .Include(s => s.Contact)
            .Include(s => s.Availability)
            .Where(s => s.Location.Coordinates != null) // Ensure coordinates exist
            .ToListAsync() // Pull data into memory to perform calculations
            .ContinueWith(task =>
            {
                var studios = task.Result;
                return studios.Where(s =>
                {
                    var dLat = ToRadians(s.Location.Coordinates.Latitude - lat);
                    var dLon = ToRadians(s.Location.Coordinates.Longitude - lng);
                    var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                            Math.Cos(ToRadians(lat)) * Math.Cos(ToRadians(s.Location.Coordinates.Latitude)) *
                            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
                    var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                    var distance = R * c;
                    return distance <= radiusKm;
                });
            });

        return nearbyStudios;
    }

    private double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }
}
