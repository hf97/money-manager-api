using money_manager_api.Entities;

namespace money_manager_api.Services
{
    public interface ITransactionTypeService
    {
        IQueryable<TransactionType> GetAll();

        Task<TransactionType> GetByIdAsync(int id);

        Task<TransactionType> CreateAsync(TransactionType transactionType);

        Task<TransactionType> PatchAsync(int id, TransactionType transactionType);

        Task<bool> DeleteAsync(int id);
    }
}
