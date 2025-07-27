using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Base;
using Shared.DTOs.MainDTOs;

namespace Presentation.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoryController(IServiceManager service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await service.Category.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await service.Category.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryDto categoryDto)
    {
        return Ok(await service.Category.CreateAsync(categoryDto));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CategoryDto categoryDto)
    {
        await service.Category.UpdateAsync(categoryDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.Category.DeleteAsync(id);
        return Ok();
    }
}

