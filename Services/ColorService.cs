using Microsoft.EntityFrameworkCore;
using money_manager_api.Entities;
using money_manager_api.Helpers;

namespace money_manager_api.Services
{
    public class ColorService : IColorService
    {
        private readonly DataContext _context;

        public ColorService(DataContext context)
        {
            _context = context;
        }

        public IQueryable<Color> GetAll()
        {
            return _context.Colors;
        }

        public async Task<Color> GetByIdAsync(Guid id)
        {
            return await _context.Colors.FindAsync(id);
        }

        public async Task<Color> CreateAsync(Color color)
        {
            _context.Colors.Add(color);
            await _context.SaveChangesAsync();

            if (!ColorExists(color.Id))
                throw new KeyNotFoundException();
            else
                return color;
        }

        public async Task<Color> PatchAsync(Guid id, Color patch)
        {
            var color = await _context.Colors.FindAsync(id);

            if (color == null)
                throw new KeyNotFoundException();

            _context.Entry(color).Property(p => p.Id).IsModified = false;
            _context.Entry(color).Property(p => p.CreatedBy).IsModified = false;
            _context.Entry(color).Property(p => p.CreatedAt).IsModified = false;
            _context.Entry(color).Property(p => p.ModifiedBy).IsModified = false;
            _context.Entry(color).Property(p => p.ModifiedAt).IsModified = false;

            color.Name = patch.Name;
            color.HexadecimalValue = patch.HexadecimalValue;

            //_context.Parameters.Update(parameter);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorExists(id))
                    throw new KeyNotFoundException();
                else
                    throw;
            }

            return color;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var color = await _context.Colors.FindAsync(id);

            if (color == null)
                throw new KeyNotFoundException();

            _context.Colors.Remove(color);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool ColorExists(Guid id)
        {
            return _context.Colors.Any(c => c.Id == id);
        }
    }
}
