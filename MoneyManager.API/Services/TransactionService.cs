using Microsoft.EntityFrameworkCore;
using money_manager_api.Entities;
using money_manager_api.Helpers;

namespace money_manager_api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _context;

        public TransactionService(DataContext context)
        {
            _context = context;
        }

        public IQueryable<Transaction> GetAll()
        {
            return _context.Transactions;
        }

        public async Task<Transaction> GetByIdAsync(Guid id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            if (!TransactionExists(transaction.Id))
                throw new KeyNotFoundException();
            else
                return transaction;
        }

        public async Task<Transaction> PatchAsync(Guid id, Transaction patch)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
                throw new KeyNotFoundException();

            _context.Entry(transaction).Property(p => p.Id).IsModified = false;
            _context.Entry(transaction).Property(p => p.CreatedBy).IsModified = false;
            _context.Entry(transaction).Property(p => p.CreatedAt).IsModified = false;
            _context.Entry(transaction).Property(p => p.ModifiedBy).IsModified = false;
            _context.Entry(transaction).Property(p => p.ModifiedAt).IsModified = false;

            transaction.UserId = patch.UserId;
            // todo test
            transaction.User = patch.User;
            transaction.TransactionTypeId = patch.TransactionTypeId;
            // todo test
            transaction.TransactionType = patch.TransactionType;
            transaction.AccountId = patch.AccountId;
            // todo test
            transaction.Account = patch.Account;
            transaction.Quantity = patch.Quantity;
            transaction.CurrencyId = patch.CurrencyId;
            // todo test
            transaction.Currency = patch.Currency;
            transaction.Date = patch.Date;
            transaction.Description = patch.Description;
            transaction.TagId = patch.TagId;
            transaction.Tag = patch.Tag;
            transaction.Beneficiary = patch.Beneficiary;
            transaction.PaymentTypeId = patch.PaymentTypeId;
            // todo test
            transaction.PaymentType = patch.PaymentType;
            transaction.Warranty = patch.Warranty;
            transaction.StatusId = patch.StatusId;
            // todo test
            transaction.Status = patch.Status;
            transaction.AddressId = patch.AddressId;
            // todo test
            transaction.Address = patch.Address;
            transaction.IsRecurrent = patch.IsRecurrent;
            transaction.UseInStatistics = patch.UseInStatistics;
            transaction.Archived = patch.Archived;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                    throw new KeyNotFoundException();
                else
                    throw;
            }

            return transaction;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
                throw new KeyNotFoundException();

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool TransactionExists(Guid id)
        {
            return _context.Transactions.Any(p => p.Id == id);
        }
    }
}
