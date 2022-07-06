using Microsoft.EntityFrameworkCore;
using money_manager_api.Entities;
using money_manager_api.Helpers;

namespace money_manager_api.Services
{
    public class StatusService : IStatusService
    {
        private readonly DataContext _context;

        public StatusService(DataContext context)
        {
            _context = context;
        }

        public IQueryable<Status> GetAll()
        {
            return _context.Statuses;
        }

        public async Task<Status> GetByIdAsync(int id)
        {
            return await _context.Statuses.FindAsync(id);
        }

        public async Task<Status> CreateAsync(Status status)
        {
            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();

            if (!StatusExists(status.Id))
                throw new KeyNotFoundException();
            else
                return status;
        }

        public async Task<Status> PatchAsync(int id, Status patch)
        {
            var status = await _context.Statuses.FindAsync(id);

            if (status == null)
                throw new KeyNotFoundException();

            _context.Entry(status).Property(p => p.Id).IsModified = false;
            _context.Entry(status).Property(p => p.CreatedBy).IsModified = false;
            _context.Entry(status).Property(p => p.CreatedAt).IsModified = false;
            _context.Entry(status).Property(p => p.ModifiedBy).IsModified = false;
            _context.Entry(status).Property(p => p.ModifiedAt).IsModified = false;

            status.Name = patch.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusExists(id))
                    throw new KeyNotFoundException();
                else
                    throw;
            }

            return status;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var status = await _context.Statuses.FindAsync(id);

            if (status == null)
                throw new KeyNotFoundException();

            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool StatusExists(int id)
        {
            return _context.Statuses.Any(s => s.Id == id);
        }
    }
}
