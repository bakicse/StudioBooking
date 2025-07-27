using AutoMapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts.Base;
using Services.Contracts.ServiceInterfaces;
using Shared.DTOs.MainDTOs;

namespace Services.Concretes.ServiceInfrastructure;
public class StudioService(IRepositoryManager repository,
    IConfiguration configuration,
    IMapper mapper,
    Serilog.ILogger logger) : IStudioService
{
    public async Task<IEnumerable<StudioDto>> GetAllStudiosAsync()
    {
        var studios = await repository.Studio.GetAllStudiosAsync();
        return studios.Select(s => MapToStudioDto(s));
    }

    public async Task<StudioDto> GetStudioByIdAsync(int id)
    {
        var studio = await repository.Studio.GetStudioByIdAsync(id);
        return studio == null ? null : MapToStudioDto(studio);
    }

    public async Task<IEnumerable<StudioDto>> SearchStudiosByAreaAsync(string area)
    {
        var studios = await repository.Studio.SearchStudiosByAreaAsync(area);
        return studios.Select(s => MapToStudioDto(s));
    }

    public async Task<IEnumerable<StudioDto>> SearchStudiosNearbyAsync(double lat, double lng, double radiusKm)
    {
        var studios = await repository.Studio.SearchStudiosNearbyAsync(lat, lng, radiusKm);
        return studios.Select(s => MapToStudioDto(s));
    }

    // Helper method to map Studio entity to StudioDto
    // In your StudioService.cs file, inside the MapToStudioDto method:

    private StudioDto MapToStudioDto(Studio studio)
    {
        return new StudioDto
        {
            Id = studio.Id,
            Name = studio.Name,
            Type = studio.Type,
            Description = studio.Description,
            PricePerHour = studio.PricePerHour,
            Currency = studio.Currency,
            Rating = studio.Rating,
            // FIX: Deserialize the JSON string from the entity into List<string> for the DTO
            // Handle nulls by providing an empty list if the string is null or empty
            Amenities = studio.Amenities != null ? System.Text.Json.JsonSerializer.Deserialize<List<string>>(studio.Amenities) : new List<string>(),
            Images = studio.Images != null ? System.Text.Json.JsonSerializer.Deserialize<List<string>>(studio.Images) : new List<string>(),
            Location = studio.Location != null ? new LocationDto
            {
                City = studio.Location.City,
                Area = studio.Location.Area,
                Address = studio.Location.Address,
                Coordinates = studio.Location.Coordinates != null ? new CoordinatesDto
                {
                    Latitude = studio.Location.Coordinates.Latitude,
                    Longitude = studio.Location.Coordinates.Longitude
                } : null
            } : null,
            Contact = studio.Contact != null ? new ContactDto
            {
                Phone = studio.Contact.Phone,
                Email = studio.Contact.Email
            } : null,
            Availability = studio.Availability != null ? new AvailabilityDto
            {
                OpenTime = studio.Availability.OpenTime,
                CloseTime = studio.Availability.CloseTime
            } : null
        };
    }
}
