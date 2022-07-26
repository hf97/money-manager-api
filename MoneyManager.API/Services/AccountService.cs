using Microsoft.EntityFrameworkCore;
using money_manager_api.Entities;
using money_manager_api.Helpers;

namespace money_manager_api.Services
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _context;

        public AccountService(DataContext context)
        {
            _context = context;
        }

        public IQueryable<Account> GetAll()
        {
            return _context.Accounts;
        }

        public async Task<Account> GetByIdAsync(Guid id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public async Task<Account> CreateAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            if (!AccountExists(account.Id))
                throw new KeyNotFoundException();
            else
                return account;
        }

        public async Task<Account> PatchAsync(Guid id, Account patch)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
                throw new KeyNotFoundException();

            _context.Entry(account).Property(p => p.Id).IsModified = false;
            _context.Entry(account).Property(p => p.CreatedBy).IsModified = false;
            _context.Entry(account).Property(p => p.CreatedAt).IsModified = false;
            _context.Entry(account).Property(p => p.ModifiedBy).IsModified = false;
            _context.Entry(account).Property(p => p.ModifiedAt).IsModified = false;

            account.UserId = patch.UserId;

            account.User = patch.User;

            account.Icon = patch.Icon;
            account.Name = patch.Name;
            account.Iban = patch.Iban;
            account.CurrencyId = patch.CurrencyId;
            //test
            account.Currency = patch.Currency;

            //test
            account.ColorId = patch.ColorId;

            //test
            account.Color = patch.Color;

            account.UseInStatistics = patch.UseInStatistics;
            account.Archived = patch.Archived;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                    throw new KeyNotFoundException();
                else
                    throw;
            }

            return account;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
                throw new KeyNotFoundException();

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool AccountExists(Guid id)
        {
            return _context.Accounts.Any(a => a.Id == id);
        }
    }
}
