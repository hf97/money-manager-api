using Microsoft.EntityFrameworkCore;
using money_manager_api.Entities;
using money_manager_api.Helpers;

namespace money_manager_api.Services
{
    public class AddressService : IAddressService
    {
        private readonly DataContext _context;

        public AddressService(DataContext context)
        {
            _context = context;
        }

        public IQueryable<Address> GetAll()
        {
            return _context.Addresses;
        }

        public async Task<Address> GetByIdAsync(Guid id)
        {
            return await _context.Addresses.FindAsync(id);
        }

        public async Task<Address> CreateAsync(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            if (!AddressExists(address.Id))
                throw new KeyNotFoundException();
            else
                return address;
        }

        public async Task<Address> PatchAsync(Guid id, Address patch)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
                throw new KeyNotFoundException();

            _context.Entry(address).Property(p => p.Id).IsModified = false;
            _context.Entry(address).Property(p => p.CreatedBy).IsModified = false;
            _context.Entry(address).Property(p => p.CreatedAt).IsModified = false;
            _context.Entry(address).Property(p => p.ModifiedBy).IsModified = false;
            _context.Entry(address).Property(p => p.ModifiedAt).IsModified = false;

            address.Name = patch.Name;
            address.Address1 = patch.Address1;
            address.PostalCode = patch.PostalCode;
            address.City = patch.City;
            address.Country = patch.Country;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                    throw new KeyNotFoundException();
                else
                    throw;
            }

            return address;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
                throw new KeyNotFoundException();

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool AddressExists(Guid id)
        {
            return _context.Addresses.Any(a => a.Id == id);
        }
    }
}
