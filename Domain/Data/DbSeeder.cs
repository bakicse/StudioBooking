using Domain.Models;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore; // Required for DbContext

namespace Domain.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
       
        if (!await context.Categories.AnyAsync())
        {
            var categories = Enumerable.Range(1, 100000)
                .Select(i => new Category
                {
                    CategoryName = $"Category {i}",
                    IsActive = true
                }).ToList();

            var bulkConfig = new BulkConfig
            {
                SetOutputIdentity = true,
            };

            await context.BulkInsertAsync(categories, bulkConfig);

            var subCategories = new List<SubCategory>();

            foreach (var category in categories)
            {
                subCategories.AddRange(
                    Enumerable.Range(1, 10) // 10 subcategories per category for example, as per your snippet
                        .Select(i => new SubCategory
                        {
                            SubCategoryName = $"SubCategory {category.Id}_{i}", // Now category.Id will be the correct DB ID
                            CategoryId = category.Id,
                            IsActive = true
                        }));
            }

            await context.BulkInsertAsync(subCategories);
        }
    }
}