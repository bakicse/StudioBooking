using Domain.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Concretes.Base;
using Repositories.Contracts.RepositoryInterfaces;

namespace Repositories.Concretes.RepositoryInfrastructure;
public class SubCategoryRepository(AppDbContext context) : BaseRepository<SubCategory>(context), ISubCategoryRepository
{
    public async Task<(List<SubCategory>, int)> GetPagedAsync(int page, int pageSize)
    {
        var query = context.SubCategories.Include(s => s.Category).AsNoTracking();
        var total = await query.CountAsync();
        var data = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new SubCategory()
            {
                Id = x.Id,
                SubCategoryName = x.SubCategoryName,
                IsActive = x.IsActive,
                Category = new Category()
                {
                    CategoryName = x.Category!.CategoryName,
                }
            })
            .ToListAsync();
        return (data, total);
    }
    public async Task<List<SubCategory>> GetListAsync()
    {
        return await context.SubCategories.OrderByDescending(x => x.IsActive).ThenByDescending(x => x.Id)
            .Select(x => new SubCategory()
            {
                Id = x.Id,
                SubCategoryName = x.SubCategoryName,
                IsActive = x.IsActive,
                Category = new Category()
                {
                    CategoryName = x.Category!.CategoryName,
                }

            }).ToListAsync();
    }

    //public Task<SubCategory?> GetByIdAsync(int id) =>
    //    context.SubCategories.Include(s => s.Category).FirstOrDefaultAsync(s => s.Id == id);

    //public async Task AddAsync(SubCategory entity)
    //{
    //    await context.SubCategories.AddAsync(entity);
    //    await context.SaveChangesAsync();
    //}

    //public async Task UpdateAsync(SubCategory entity)
    //{
    //    context.SubCategories.Update(entity);
    //    await context.SaveChangesAsync();
    //}

    //public async Task DeleteAsync(int id)
    //{
    //    var entity = await context.SubCategories.FindAsync(id);
    //    if (entity != null)
    //    {
    //        context.SubCategories.Remove(entity);
    //        await context.SaveChangesAsync();
    //    }
    //}
}

