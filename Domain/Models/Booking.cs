namespace Domain.Models;

public  class Booking
{
    public int Id { get; set; }

    public int StudioId { get; set; }

    public string UserName { get; set; } = null!;

    public string? Email { get; set; }

    public DateTime BookingDate { get; set; }

    public string TimeSlot { get; set; } = null!;

    public virtual Studio Studio { get; set; } = null!;
}
