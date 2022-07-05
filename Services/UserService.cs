using Microsoft.EntityFrameworkCore;
using money_manager_api.Entities;
using money_manager_api.Helpers;

namespace money_manager_api.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            if (!UserExists(user.Id))
                throw new KeyNotFoundException();
            else
                return user;
        }

        public async Task<User> PatchAsync(Guid id, User patch)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                throw new KeyNotFoundException();

            _context.Entry(user).Property(p => p.Id).IsModified = false;
            _context.Entry(user).Property(p => p.CreatedBy).IsModified = false;
            _context.Entry(user).Property(p => p.CreatedAt).IsModified = false;
            _context.Entry(user).Property(p => p.ModifiedBy).IsModified = false;
            _context.Entry(user).Property(p => p.ModifiedAt).IsModified = false;

            user.Email = patch.Email;
            user.FirstName = patch.FirstName;
            user.LastName = patch.LastName;
            user.Username = patch.Username;
            user.JoiningAt = patch.JoiningAt;
            user.PermissionId = patch.PermissionId;
            // todo test
            user.Permission = patch.Permission;
            user.PasswordHash = patch.PasswordHash;
            user.PasswordSalt = patch.PasswordSalt;
            user.RecoverToken = patch.RecoverToken;
            user.RecoverExpiration = patch.RecoverExpiration;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                    throw new KeyNotFoundException();
                else
                    throw;
            }

            return user;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                throw new KeyNotFoundException();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(p => p.Id == id);
        }
    }
}
