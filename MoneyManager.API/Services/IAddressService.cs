using money_manager_api.Entities;

namespace money_manager_api.Services
{
    public interface IAddressService
    {
        IQueryable<Address> GetAll();

        Task<Address> GetByIdAsync(Guid id);

        Task<Address> CreateAsync(Address address);

        Task<Address> PatchAsync(Guid id, Address address);

        Task<bool> DeleteAsync(Guid id);
    }
}
