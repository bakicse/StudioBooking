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
[Route("api/[controller]")] // e.g., /api/bookings
public class BookingsController(IServiceManager service) : ControllerBase
{
    // POST /api/bookings - Create a new booking
    [HttpPost]
    [ProducesResponseType(typeof(BookingDto), 201)]
    [ProducesResponseType(400)] // Bad Request for validation errors
    [ProducesResponseType(409)] // Conflict for booking overlaps
    [ProducesResponseType(404)] // Not Found for studio not existing
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var booking = await service.Booking.CreateBookingAsync(request);
            // The CreatedAtAction needs a route name and route values for the GET endpoint
            // Since there's no specific GET /api/bookings/{id} defined for retrieving a single booking
            // from the original spec, I'll use a fallback to GetAllBookings for simplicity.
            // In a real application, you should implement a GET /{id} endpoint for bookings.
            return CreatedAtAction(nameof(GetAllBookings), null, booking);
        }
        catch (ArgumentException ex) // For Studio not found or invalid time slot format/range
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex) // For booking overlaps
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            // Log the exception (e.g., using ILogger)
            // _logger.LogError(ex, "Error creating booking.");
            return StatusCode(500, "An error occurred while creating the booking: " + ex.Message);
        }
    }

    // GET /api/bookings - Get all bookings (for testing and display)
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookingDto>), 200)]
    public async Task<IActionResult> GetAllBookings()
    {
        var bookings = await service.Booking.GetAllBookingsAsync();
        return Ok(bookings);
    }

    // GET /api/studios/{id}/availability?date={date} - Get available time slots for a studio on a specific date
    [HttpGet("/api/studios/{studioId}/availability")] // Override base route for specific path
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
        catch (ArgumentException ex) // For Studio not found
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            // Log the exception
            // _logger.LogError(ex, "Error fetching available slots.");
            return StatusCode(500, "An error occurred while fetching available slots: " + ex.Message);
        }
    }
}