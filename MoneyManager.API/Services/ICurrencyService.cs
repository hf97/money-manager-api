using money_manager_api.Entities;

namespace money_manager_api.Services
{
    public interface ICurrencyService
    {
        IQueryable<Currency> GetAll();

        Task<Currency> GetByIdAsync(Guid id);

        Task<Currency> GetByIsoAsync(string key);

        Task<Currency> CreateAsync(Currency currency);

        Task<Currency> PatchAsync(Guid id, Currency currency);

        Task<bool> DeleteAsync(Guid id);
    }
}
