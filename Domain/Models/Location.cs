namespace Domain.Models;

public  class Location
{
    public int Id { get; set; }

    public string? City { get; set; }

    public string? Area { get; set; }

    public string? Address { get; set; }

    public int? CoordinatesId { get; set; }

    public virtual Coordinate? Coordinates { get; set; }

    public virtual ICollection<Studio> Studios { get; set; } = new List<Studio>();
}
