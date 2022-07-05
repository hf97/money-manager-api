using Microsoft.EntityFrameworkCore;
using money_manager_api.Entities;
using money_manager_api.Helpers;

namespace money_manager_api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Categories;
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            if (!CategoryExists(category.Id))
                throw new KeyNotFoundException();
            else
                return category;
        }

        public async Task<Category> PatchAsync(Guid id, Category patch)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                throw new KeyNotFoundException();

            _context.Entry(category).Property(p => p.Id).IsModified = false;
            _context.Entry(category).Property(p => p.CreatedBy).IsModified = false;
            _context.Entry(category).Property(p => p.CreatedAt).IsModified = false;
            _context.Entry(category).Property(p => p.ModifiedBy).IsModified = false;
            _context.Entry(category).Property(p => p.ModifiedAt).IsModified = false;

            category.Name = patch.Name;
            category.Icon = patch.Icon;
            category.ColorId = patch.ColorId;
            // todo test
            category.Color = patch.Color;

            //_context.Parameters.Update(parameter);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                    throw new KeyNotFoundException();
                else
                    throw;
            }

            return category;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                throw new KeyNotFoundException();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }
    }
}
