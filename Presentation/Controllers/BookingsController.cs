using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Base;
using Shared.DTOs.MainDTOs;

namespace Presentation.API.Controllers;

[ApiController]
[Route("api/Bookings")]
public class BookingsController(IServiceManager service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(BookingDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var booking = await service.Booking.CreateBookingAsync(request);
            return CreatedAtAction(nameof(GetAllBookings), null, booking);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while creating the booking: " + ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookingDto>), 200)]
    public async Task<IActionResult> GetAllBookings()
    {
        var bookings = await service.Booking.GetAllBookingsAsync();
        return Ok(bookings);
    }

    [HttpGet("/api/studios/{studioId}/availability")]
    [ProducesResponseType(typeof(IEnumerable<AvailableTimeSlotDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAvailableTimeSlots(int studioId, [FromQuery] DateTime date)
    {
        if (date == default(DateTime))
        {
            return BadRequest("Date parameter is required and must be a valid date format (e.g., YYYY-MM-DD).");
        }
        if (date.Date < DateTime.Today.Date)
        {
            return BadRequest("Cannot retrieve availability for past dates.");
        }

        try
        {
            var availableSlots = await service.Booking.GetAvailableTimeSlotsAsync(studioId, date);
            return Ok(availableSlots);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while fetching available slots: " + ex.Message);
        }
    }
}
