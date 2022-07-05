using Microsoft.EntityFrameworkCore;
using money_manager_api.Entities;
using money_manager_api.Helpers;

namespace money_manager_api.Services
{
    public class TransactionTypeService : ITransactionTypeService
    {
        private readonly DataContext _context;

        public TransactionTypeService(DataContext context)
        {
            _context = context;
        }

        public IQueryable<TransactionType> GetAll()
        {
            return _context.TransactionTypes;
        }

        public async Task<TransactionType> GetByIdAsync(int id)
        {
            return await _context.TransactionTypes.FindAsync(id);
        }

        public async Task<TransactionType> CreateAsync(TransactionType transactionType)
        {
            _context.TransactionTypes.Add(transactionType);
            await _context.SaveChangesAsync();

            if (!TransactionTypeExists(transactionType.Id))
                throw new KeyNotFoundException();
            else
                return transactionType;
        }

        public async Task<TransactionType> PatchAsync(int id, TransactionType patch)
        {
            var transactionType = await _context.TransactionTypes.FindAsync(id);

            if (transactionType == null)
                throw new KeyNotFoundException();

            _context.Entry(transactionType).Property(p => p.Id).IsModified = false;
            _context.Entry(transactionType).Property(p => p.CreatedBy).IsModified = false;
            _context.Entry(transactionType).Property(p => p.CreatedAt).IsModified = false;
            _context.Entry(transactionType).Property(p => p.ModifiedBy).IsModified = false;
            _context.Entry(transactionType).Property(p => p.ModifiedAt).IsModified = false;

            transactionType.Name = patch.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionTypeExists(id))
                    throw new KeyNotFoundException();
                else
                    throw;
            }

            return transactionType;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var transactionType = await _context.TransactionTypes.FindAsync(id);

            if (transactionType == null)
                throw new KeyNotFoundException();

            _context.TransactionTypes.Remove(transactionType);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool TransactionTypeExists(int id)
        {
            return _context.TransactionTypes.Any(p => p.Id == id);
        }
    }
}
