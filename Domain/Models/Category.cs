namespace Domain.Models;
public class Category
{
    public int Id { get; set; }
    public required string CategoryName { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
