namespace Domain.Models;

public  class Studio
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Type { get; set; }

    public string? Description { get; set; }

    public double PricePerHour { get; set; }

    public string? Currency { get; set; }

    public double Rating { get; set; }

    public string? Amenities { get; set; }

    public string? Images { get; set; }

    public int? LocationId { get; set; }

    public int? ContactId { get; set; }

    public int? AvailabilityId { get; set; }

    public virtual Availability? Availability { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Contact? Contact { get; set; }

    public virtual Location? Location { get; set; }
}
