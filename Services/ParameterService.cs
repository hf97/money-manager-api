using Microsoft.EntityFrameworkCore;
using money_manager_api.Entities;
using money_manager_api.Helpers;

namespace money_manager_api.Services
{
    public class ParameterService : IParameterService
    {
        private readonly DataContext _context;

        public ParameterService(DataContext context)
        {
            _context = context;
        }

        public IQueryable<Parameter> GetAll()
        {
            return _context.Parameters;
        }

        public async Task<Parameter> GetByIdAsync(Guid id)
        {
            return await _context.Parameters.FindAsync(id);
        }

        public async Task<Parameter> GetByKeyAsync(string key)
        {
            return await _context.Parameters.FirstOrDefaultAsync(k => k.Key == key);
        }

        public async Task<Parameter> CreateAsync(Parameter parameter)
        {
            _context.Parameters.Add(parameter);
            await _context.SaveChangesAsync();

            if (!ParameterExists(parameter.Id))
                throw new KeyNotFoundException();
            else
                return parameter;
        }

        public async Task<Parameter> PatchAsync(Guid id, Parameter patch)
        {
            var parameter = await _context.Parameters.FindAsync(id);

            if (parameter == null)
                throw new KeyNotFoundException();

            _context.Entry(parameter).Property(p => p.Id).IsModified = false;
            _context.Entry(parameter).Property(p => p.CreatedBy).IsModified = false;
            _context.Entry(parameter).Property(p => p.CreatedAt).IsModified = false;
            _context.Entry(parameter).Property(p => p.ModifiedBy).IsModified = false;
            _context.Entry(parameter).Property(p => p.ModifiedAt).IsModified = false;

            parameter.Key = patch.Key;
            parameter.Value = patch.Value;
            parameter.Description = patch.Description;

            //_context.Parameters.Update(parameter);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParameterExists(id))
                    throw new KeyNotFoundException();
                else
                    throw;
            }

            return parameter;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var parameter = await _context.Parameters.FindAsync(id);

            if (parameter == null)
                throw new KeyNotFoundException();

            _context.Parameters.Remove(parameter);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool ParameterExists(Guid id)
        {
            return _context.Parameters.Any(p => p.Id == id);
        }
    }
}
