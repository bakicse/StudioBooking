namespace Shared.DTOs.MainDTOs;
public class BookingDto
{
    public int Id { get; set; }
    public int StudioId { get; set; }
    public string StudioName { get; set; } // Denormalized for simpler DTO
    public DateTime BookingDate { get; set; }
    public string TimeSlot { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}
