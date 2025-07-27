using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Contracts.Base;
using Shared.DTOs.MainDTOs;
using Shared.DTOs.ViewModels;

namespace Presentation.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class SubCategoryController(IServiceManager service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PagedResultDto<SubCategoryViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)] // For unexpected errors
    public async Task<IActionResult> GetSubCategories(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        if (page < 1)
        {
            return BadRequest("Page number must be 1 or greater.");
        }
        if (pageSize < 1 || pageSize > 100) // Example: Limit page size to prevent abuse
        {
            return BadRequest("Page size must be between 1 and 100.");
        }

        try
        {
            var (subCategories, totalCount) = await service.SubCategory.GetPagedAsync(page, pageSize);

            var pagedResult = new PagedResultDto<SubCategoryViewModel>
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = subCategories
            };

            return Ok(pagedResult);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while fetching paged subcategories.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await service.SubCategory.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await service.SubCategory.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SubCategoryDto subCategoryDto)
    {
        return Ok(await service.SubCategory.CreateAsync(subCategoryDto));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SubCategoryDto subCategoryDto)
    {
        await service.SubCategory.UpdateAsync(subCategoryDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.SubCategory.DeleteAsync(id);
        return Ok();
    }
}

