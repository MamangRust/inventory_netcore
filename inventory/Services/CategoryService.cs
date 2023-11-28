using inventory.Entities;
using inventory.Helpers;
using Microsoft.EntityFrameworkCore;

namespace inventory.Services;

public interface ICategoryService
{
    Task<Category> GetCategoryById(int id);
    Task<IEnumerable<Category>> GetAllCategories();
    Task<Category> CreateCategory(Category category);
    Task<Category> UpdateCategory(int id, Category category);
    Task<bool> DeleteCategory(int id);
}

public class CategoryService : ICategoryService
{
    private readonly DataContext _context;

    public CategoryService(DataContext context)
    {
        _context = context;
    }

    public async Task<Category> GetCategoryById(int id)
    {
        try
        {
            return await _context.Categories.FindAsync(id);

        }
        catch (Exception e)
        {
            throw new ApplicationException("Error " + e);
        }
    }

    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> CreateCategory(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateCategory(int id, Category category)
    {
        var existingCategory = await _context.Categories.FindAsync(id);
        if (existingCategory == null)
        {
            throw new ApplicationException("Error Update Category " + id);
        }

        existingCategory.Name = category.Name;

        await _context.SaveChangesAsync();
        return existingCategory;
    }

    public async Task<bool> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return false;
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}
