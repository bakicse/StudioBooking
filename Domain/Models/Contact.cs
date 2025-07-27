namespace Domain.Models;

public  class Contact
{
    public int Id { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Studio> Studios { get; set; } = new List<Studio>();
}
