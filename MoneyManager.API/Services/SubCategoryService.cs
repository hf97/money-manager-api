using Microsoft.EntityFrameworkCore;
using money_manager_api.Entities;
using money_manager_api.Helpers;

namespace money_manager_api.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly DataContext _context;

        public SubCategoryService(DataContext context)
        {
            _context = context;
        }

        public IQueryable<SubCategory> GetAll()
        {
            return _context.SubCategories;
        }

        public async Task<SubCategory> GetByIdAsync(Guid id)
        {
            return await _context.SubCategories.FindAsync(id);
        }

        public async Task<SubCategory> CreateAsync(SubCategory subCategory)
        {
            _context.SubCategories.Add(subCategory);
            await _context.SaveChangesAsync();

            if (!SubCategoryExists(subCategory.Id))
                throw new KeyNotFoundException();
            else
                return subCategory;
        }

        public async Task<SubCategory> PatchAsync(Guid id, SubCategory patch)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);

            if (subCategory == null)
                throw new KeyNotFoundException();

            _context.Entry(subCategory).Property(p => p.Id).IsModified = false;
            _context.Entry(subCategory).Property(p => p.CreatedBy).IsModified = false;
            _context.Entry(subCategory).Property(p => p.CreatedAt).IsModified = false;
            _context.Entry(subCategory).Property(p => p.ModifiedBy).IsModified = false;
            _context.Entry(subCategory).Property(p => p.ModifiedAt).IsModified = false;

            subCategory.Name = patch.Name;
            subCategory.Icon = patch.Icon;
            subCategory.ColorId = patch.ColorId;
            // todo test
            subCategory.Color = patch.Color;
            subCategory.CategoryId = patch.CategoryId;
            // todo test
            subCategory.Category = patch.Category;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoryExists(id))
                    throw new KeyNotFoundException();
                else
                    throw;
            }

            return subCategory;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);

            if (subCategory == null)
                throw new KeyNotFoundException();

            _context.SubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool SubCategoryExists(Guid id)
        {
            return _context.SubCategories.Any(c => c.Id == id);
        }
    }
}
