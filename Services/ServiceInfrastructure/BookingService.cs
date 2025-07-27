using AutoMapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts.Base;
using Services.Contracts.ServiceInterfaces;
using Shared.DTOs.MainDTOs;

namespace Services.Concretes.ServiceInfrastructure;
public class BookingService(IRepositoryManager repository,
    IConfiguration configuration,
    IMapper mapper,
    Serilog.ILogger logger) : IBookingService
{
    public async Task<BookingDto> CreateBookingAsync(CreateBookingRequestDto request)
    {
        // 1. Validate studio exists
        var studio = await repository.Studio.GetStudioByIdAsync(request.StudioId);
        if (studio == null)
        {
            throw new ArgumentException("Studio not found.");
        }

        // Parse the requested TimeSlot string into StartTime and EndTime for validation
        if (!TryParseTimeSlot(request.TimeSlot, out TimeSpan requestedStartTime, out TimeSpan requestedEndTime))
        {
            throw new ArgumentException("Invalid TimeSlot format. Expected 'HH:MM-HH:MM'.");
        }

        // Check if requested time is within studio's general open/close times
        if (requestedStartTime < studio.Availability!.OpenTime.ToTimeSpan() || requestedEndTime > studio.Availability!.CloseTime.ToTimeSpan())
        {
            throw new ArgumentException("Requested booking time is outside studio's general availability.");
        }

        // Check for overlaps with existing bookings for that studio and date
        var existingBookings = await repository.Booking.GetBookingsForStudioAndDateAsync(request.StudioId,request.BookingDate);

        foreach (var existingBooking in existingBookings)
        {
            if (!TryParseTimeSlot(existingBooking.TimeSlot, out TimeSpan existingStartTime, out TimeSpan existingEndTime))
            {
                // This should ideally not happen if data is clean, but handle corrupted data
                Console.WriteLine($"Warning: Could not parse existing booking TimeSlot: {existingBooking.TimeSlot}");
                continue;
            }

            // Check for overlap: (requestedStart < existingEnd) and (requestedEnd > existingStart)
            if (requestedStartTime < existingEndTime && requestedEndTime > existingStartTime)
            {
                throw new InvalidOperationException($"Requested time slot '{request.TimeSlot}' overlaps with an existing booking '{existingBooking.TimeSlot}'.");
            }
        }

        // 2. Create the booking entity
        var newBooking = new Booking
        {
            StudioId = request.StudioId,
            BookingDate = request.BookingDate.Date, // Ensure only date part is stored
            TimeSlot = request.TimeSlot,
            UserName = request.UserName,
            Email = request.Email
        };

        // 3. Save to repository
        var createdBooking = await repository.Booking.CreateBookingAsync(newBooking);

        // 4. Map to DTO and return
        return MapToBookingDto(createdBooking);
    }

    public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
    {
        var bookings = await  repository.Booking.GetAllBookingsAsync();
        return bookings.Select(b => MapToBookingDto(b));
    }

    public async Task<IEnumerable<AvailableTimeSlotDto>> GetAvailableTimeSlotsAsync(int studioId, DateTime date)
    {
        var studio = await repository.Studio.GetStudioByIdAsync(studioId);
        if (studio == null)
        {
            throw new ArgumentException("Studio not found.");
        }

        var bookedSlots = await repository.Booking.GetBookingsForStudioAndDateAsync(studioId, date);

        var availableSlots = new List<AvailableTimeSlotDto>();

        var current = studio.Availability.OpenTime.ToTimeSpan();
        var close = studio.Availability.CloseTime.ToTimeSpan();

        // Define a granularity for checking, e.g., 30 minutes or 1 hour
        TimeSpan slotDuration = TimeSpan.FromHours(1); // Example: 1-hour slots

        while (current < close)
        {
            var potentialSlotEnd = current.Add(slotDuration);

            // Ensure the potential slot doesn't exceed closing time
            if (potentialSlotEnd > close)
            {
                // If the remaining time is less than a full slot, we can decide
                // whether to offer it as a smaller slot or just stop.
                break;
            }

            bool isBooked = false;
            foreach (var booked in bookedSlots)
            {
                if (!TryParseTimeSlot(booked.TimeSlot, out TimeSpan bookedStartTime, out TimeSpan bookedEndTime))
                {
                    Console.WriteLine($"Warning: Could not parse existing booking TimeSlot: {booked.TimeSlot}");
                    continue;
                }

                // Check for overlap: (potentialStart < bookedEnd) and (potentialEnd > bookedStart)
                if (current < bookedEndTime && potentialSlotEnd > bookedStartTime)
                {
                    isBooked = true;
                    // If there's an overlap, jump 'current' past the end of the booked slot
                    current = bookedEndTime;
                    // Adjust potentialSlotEnd based on the new 'current'
                    potentialSlotEnd = current.Add(slotDuration);
                    break; // Exit inner loop, re-evaluate from new 'current'
                }
            }

            if (!isBooked)
            {
                availableSlots.Add(new AvailableTimeSlotDto
                {
                    StartTime = TimeOnly.FromTimeSpan(current),
                    EndTime = TimeOnly.FromTimeSpan(potentialSlotEnd)
                });
                current = potentialSlotEnd; // Move to the next slot
            }
        }

        return availableSlots;
    }

    // Helper method to map Booking entity to BookingDto
    private BookingDto MapToBookingDto(Booking booking)
    {
        return new BookingDto
        {
            Id = booking.Id,
            StudioId = booking.StudioId,
            StudioName = booking.Studio?.Name, // Null check in case Studio is not included
            BookingDate = booking.BookingDate,
            TimeSlot = booking.TimeSlot,
            UserName = booking.UserName,
            Email = booking.Email
        };
    }

    // Helper method to parse "HH:MM-HH:MM" format
    private bool TryParseTimeSlot(string timeSlot, out TimeSpan startTime, out TimeSpan endTime)
    {
        startTime = TimeSpan.Zero;
        endTime = TimeSpan.Zero;

        if (string.IsNullOrWhiteSpace(timeSlot)) return false;

        var parts = timeSlot.Split('-');
        if (parts.Length != 2) return false;

        if (!TimeSpan.TryParse(parts[0].Trim(), out startTime)) return false;
        if (!TimeSpan.TryParse(parts[1].Trim(), out endTime)) return false;

        return true;
    }
}
