namespace Domain.Models;
public class SubCategory
{
    public int Id { get; set; }
    public required string SubCategoryName { get; set; }
    public int CategoryId { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public virtual Category? Category { get; set; }
}
