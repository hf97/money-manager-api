using money_manager_api.Entities;

namespace money_manager_api.Services
{
    public interface IAccountService
    {
        IQueryable<Account> GetAll();

        Task<Account> GetByIdAsync(Guid id);

        Task<Account> CreateAsync(Account account);

        Task<Account> PatchAsync(Guid id, Account account);

        Task<bool> DeleteAsync(Guid id);
    }
}
