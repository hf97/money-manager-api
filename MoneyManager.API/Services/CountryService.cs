using Microsoft.EntityFrameworkCore;
using money_manager_api.Entities;
using money_manager_api.Helpers;

namespace money_manager_api.Services
{
    public class CountryService : ICountryService
    {
        private readonly DataContext _context;

        public CountryService(DataContext context)
        {
            _context = context;
        }

        public IQueryable<Country> GetAll()
        {
            return _context.Countries;
        }

        public async Task<Country> GetByIdAsync(Guid id)
        {
            return await _context.Countries.FindAsync(id);
        }

        public async Task<Country> GetByIsoAsync(string iso)
        {
            return await _context.Countries.FirstOrDefaultAsync(c => c.Iso == iso);
        }

        public async Task<Country> CreateAsync(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            if (!CountryExists(country.Id))
                throw new KeyNotFoundException();
            else
                return country;
        }

        public async Task<Country> PatchAsync(Guid id, Country patch)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
                throw new KeyNotFoundException();

            _context.Entry(country).Property(p => p.Id).IsModified = false;
            _context.Entry(country).Property(p => p.CreatedBy).IsModified = false;
            _context.Entry(country).Property(p => p.CreatedAt).IsModified = false;
            _context.Entry(country).Property(p => p.ModifiedBy).IsModified = false;
            _context.Entry(country).Property(p => p.ModifiedAt).IsModified = false;

            country.Iso = patch.Iso;
            country.Name = patch.Name;
            country.Culture = patch.Culture;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                    throw new KeyNotFoundException();
                else
                    throw;
            }

            return country;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
                throw new KeyNotFoundException();

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool CountryExists(Guid id)
        {
            return _context.Countries.Any(p => p.Id == id);
        }
    }
}
