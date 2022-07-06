using Microsoft.EntityFrameworkCore;
using money_manager_api.Entities;
using money_manager_api.Helpers;

namespace money_manager_api.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly DataContext _context;

        public PaymentTypeService(DataContext context)
        {
            _context = context;
        }

        public IQueryable<PaymentType> GetAll()
        {
            return _context.PaymentTypes;
        }

        public async Task<PaymentType> GetByIdAsync(int id)
        {
            return await _context.PaymentTypes.FindAsync(id);
        }

        public async Task<PaymentType> CreateAsync(PaymentType paymentType)
        {
            _context.PaymentTypes.Add(paymentType);
            await _context.SaveChangesAsync();

            if (!ParameterExists(paymentType.Id))
                throw new KeyNotFoundException();
            else
                return paymentType;
        }

        public async Task<PaymentType> PatchAsync(int id, PaymentType patch)
        {
            var paymentType = await _context.PaymentTypes.FindAsync(id);

            if (paymentType == null)
                throw new KeyNotFoundException();

            _context.Entry(paymentType).Property(p => p.Id).IsModified = false;
            _context.Entry(paymentType).Property(p => p.CreatedBy).IsModified = false;
            _context.Entry(paymentType).Property(p => p.CreatedAt).IsModified = false;
            _context.Entry(paymentType).Property(p => p.ModifiedBy).IsModified = false;
            _context.Entry(paymentType).Property(p => p.ModifiedAt).IsModified = false;

            paymentType.Name = patch.Name;

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

            return paymentType;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var paymentType = await _context.PaymentTypes.FindAsync(id);

            if (paymentType == null)
                throw new KeyNotFoundException();

            _context.PaymentTypes.Remove(paymentType);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool ParameterExists(int id)
        {
            return _context.PaymentTypes.Any(p => p.Id == id);
        }
    }
}
