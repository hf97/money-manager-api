using Microsoft.EntityFrameworkCore;
using money_manager_api.Entities;
using money_manager_api.Helpers;

namespace money_manager_api.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly DataContext _context;

        public PermissionService(DataContext context)
        {
            _context = context;
        }

        public IQueryable<Permission> GetAll()
        {
            return _context.Permissions;
        }

        public async Task<Permission> GetByIdAsync(int id)
        {
            return await _context.Permissions.FindAsync(id);
        }

        public async Task<Permission> CreateAsync(Permission permission)
        {
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();

            if (!PermissionExists(permission.Id))
                throw new KeyNotFoundException();
            else
                return permission;
        }

        public async Task<Permission> PatchAsync(int id, Permission patch)
        {
            var permission = await _context.Permissions.FindAsync(id);

            if (permission == null)
                throw new KeyNotFoundException();

            _context.Entry(permission).Property(p => p.Id).IsModified = false;
            _context.Entry(permission).Property(p => p.CreatedBy).IsModified = false;
            _context.Entry(permission).Property(p => p.CreatedAt).IsModified = false;
            _context.Entry(permission).Property(p => p.ModifiedBy).IsModified = false;
            _context.Entry(permission).Property(p => p.ModifiedAt).IsModified = false;

            permission.Name = patch.Name;
            permission.Description = patch.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionExists(id))
                    throw new KeyNotFoundException();
                else
                    throw;
            }

            return permission;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);

            if (permission == null)
                throw new KeyNotFoundException();

            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool PermissionExists(int id)
        {
            return _context.Permissions.Any(p => p.Id == id);
        }
    }
}
