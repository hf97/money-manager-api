using money_manager_api.Entities;

namespace money_manager_api.Services
{
    public interface ITransactionService
    {
        IQueryable<Transaction> GetAll();

        Task<Transaction> GetByIdAsync(Guid id);

        Task<Transaction> CreateAsync(Transaction transaction);

        Task<Transaction> PatchAsync(Guid id, Transaction transaction);

        Task<bool> DeleteAsync(Guid id);
    }
}
