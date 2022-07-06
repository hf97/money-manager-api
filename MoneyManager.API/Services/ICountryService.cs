using money_manager_api.Entities;

namespace money_manager_api.Services
{
    public interface ICountryService
    {
        IQueryable<Country> GetAll();

        Task<Country> GetByIdAsync(Guid id);

        Task<Country> GetByIsoAsync(string iso);

        Task<Country> CreateAsync(Country country);

        Task<Country> PatchAsync(Guid id, Country country);

        Task<bool> DeleteAsync(Guid id);
    }
}
