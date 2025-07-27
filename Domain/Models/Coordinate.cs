namespace Domain.Models;

public  class Coordinate
{
    public int Id { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}
