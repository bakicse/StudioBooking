namespace Domain.Models;

public  class Availability
{
    public int Id { get; set; }

    public TimeOnly OpenTime { get; set; }

    public TimeOnly CloseTime { get; set; }

    public virtual ICollection<Studio> Studios { get; set; } = new List<Studio>();
}
