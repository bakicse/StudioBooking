namespace Shared.DTOs.MainDTOs;
public class StudioDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public double PricePerHour { get; set; }
    public string Currency { get; set; }
    public double Rating { get; set; }
    public List<string> Amenities { get; set; }
    public List<string> Images { get; set; }
    public LocationDto Location { get; set; }
    public ContactDto Contact { get; set; }
    public AvailabilityDto Availability { get; set; }
}
