using Domain.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Concretes.Base;
using Repositories.Contracts.RepositoryInterfaces;

namespace Repositories.Concretes.RepositoryInfrastructure;
public class CategoryRepository(AppDbContext context) :BaseRepository<Category>(context), ICategoryRepository
{
    public async Task<(List<Category>, int)> GetPagedAsync(int page, int pageSize)
    {
        var query = context.Categories.AsNoTracking();
        var total = await query.CountAsync();

        var data = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (data, total);
    }

    public async Task<List<Category>> GetListAsync()
    {
        var categories =  await context.Categories
            .OrderByDescending(x => x.IsActive)
            .ThenByDescending(x => x.Id)
            .Select(x => new Category { Id = x.Id, CategoryName = x.CategoryName, IsActive = x.IsActive })
            .ToListAsync();
        return categories;
    }

    //public async Task<Category?> GetByIdAsync(int id)
    //{
    //    return await context.Categories.FindAsync(id);
    //}

    //public async Task AddAsync(Category category)
    //{
    //    await context.Categories.AddAsync(category);
    //    await context.SaveChangesAsync();
    //}

    //public async Task UpdateAsync(Category category)
    //{
    //    context.Categories.Update(category);
    //    await context.SaveChangesAsync();
    //}

    //public async Task DeleteAsync(int id)
    //{
    //    var category = await context.Categories.FindAsync(id);
    //    if (category != null)
    //    {
    //        context.Categories.Remove(category);
    //        await context.SaveChangesAsync();
    //    }
    //}
}

