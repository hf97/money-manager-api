using Microsoft.EntityFrameworkCore;
using money_manager_api.Entities;
using money_manager_api.Helpers;

namespace money_manager_api.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly DataContext _context;

        public CurrencyService(DataContext context)
        {
            _context = context;
        }

        public IQueryable<Currency> GetAll()
        {
            return _context.Currencies;
        }

        public async Task<Currency> GetByIdAsync(Guid id)
        {
            return await _context.Currencies.FindAsync(id);
        }

        public async Task<Currency> GetByIsoAsync(string iso)
        {
            return await _context.Currencies.FirstOrDefaultAsync(c => c.Iso == iso);
        }

        public async Task<Currency> CreateAsync(Currency currency)
        {
            _context.Currencies.Add(currency);
            await _context.SaveChangesAsync();

            if (!ParameterExists(currency.Id))
                throw new KeyNotFoundException();
            else
                return currency;
        }

        public async Task<Currency> PatchAsync(Guid id, Currency patch)
        {
            var parameter = await _context.Currencies.FindAsync(id);

            if (parameter == null)
                throw new KeyNotFoundException();

            _context.Entry(parameter).Property(p => p.Id).IsModified = false;
            _context.Entry(parameter).Property(p => p.CreatedBy).IsModified = false;
            _context.Entry(parameter).Property(p => p.CreatedAt).IsModified = false;
            _context.Entry(parameter).Property(p => p.ModifiedBy).IsModified = false;
            _context.Entry(parameter).Property(p => p.ModifiedAt).IsModified = false;

            parameter.Iso = patch.Iso;
            parameter.Name = patch.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParameterExists(id))
                    throw new KeyNotFoundException();
                else
                    throw;
            }

            return parameter;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var currency = await _context.Currencies.FindAsync(id);

            if (currency == null)
                throw new KeyNotFoundException();

            _context.Currencies.Remove(currency);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool ParameterExists(Guid id)
        {
            return _context.Currencies.Any(c => c.Id == id);
        }
    }
}
