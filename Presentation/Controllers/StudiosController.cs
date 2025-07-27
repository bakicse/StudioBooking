using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Base;
using Shared.DTOs.MainDTOs;

namespace Presentation.API.Controllers;

[ApiController]
[Route("api/Studios")]
public class StudiosController(IServiceManager service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<StudioDto>), 200)]
    public async Task<IActionResult> GetAllStudios()
    {
        var studios = await service.Studio.GetAllStudiosAsync();
        return Ok(studios);
    }

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

    [HttpGet("nearby")]
    [ProducesResponseType(typeof(IEnumerable<StudioDto>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> SearchStudiosNearby(
        [FromQuery] double lat,
        [FromQuery] double lng,
        [FromQuery] double radius)
    {
        if (radius <= 0)
        {
            return BadRequest("Radius must be a positive value.");
        }

        if (lat < -90 || lat > 90 || lng < -180 || lng > 180)
        {
            return BadRequest("Invalid latitude or longitude values.");
        }

        var studios = await service.Studio.SearchStudiosNearbyAsync(lat, lng, radius);
        return Ok(studios);
    }
}
