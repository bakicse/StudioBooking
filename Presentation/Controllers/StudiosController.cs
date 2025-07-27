using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Base;
using Services.Contracts.ServiceInterfaces;
using Shared.DTOs.MainDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.API.Controllers;
[ApiController]
[Route("api/[controller]")] // e.g., /api/studios
public class StudiosController(IServiceManager service) : ControllerBase
{
    // GET /api/studios - Get all studios
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<StudioDto>), 200)]
    public async Task<IActionResult> GetAllStudios()
    {
        var studios = await service.Studio.GetAllStudiosAsync();
        return Ok(studios);
    }

    // GET /api/studios/{id} - Get details of a specific studio
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(StudioDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetStudioById(int id)
    {
        var studio = await service.Studio.GetStudioByIdAsync(id);
        if (studio == null)
        {
            return NotFound();
        }
        return Ok(studio);
    }

    // GET /api/studios/search?area={area} - Search studios by area
    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<StudioDto>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> SearchStudiosByArea([FromQuery] string area)
    {
        if (string.IsNullOrWhiteSpace(area))
        {
            return BadRequest("Area parameter is required for search.");
        }
        var studios = await service.Studio.SearchStudiosByAreaAsync(area);
        return Ok(studios);
    }

    // GET /api/studios/nearby?lat={lat}&lng={lng}&radius={km} - Search studios within a radius from current location
    [HttpGet("nearby")]
    [ProducesResponseType(typeof(IEnumerable<StudioDto>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> SearchStudiosNearby(
        [FromQuery] double lat,
        [FromQuery] double lng,
        [FromQuery] double radius) // Assuming radius is in km as per requirement
    {
        if (radius <= 0)
        {
            return BadRequest("Radius must be a positive value.");
        }
        // Basic validation for lat/lng range
        if (lat < -90 || lat > 90 || lng < -180 || lng > 180)
        {
            return BadRequest("Invalid latitude or longitude values.");
        }

        var studios = await service.Studio.SearchStudiosNearbyAsync(lat, lng, radius);
        return Ok(studios);
    }
}
