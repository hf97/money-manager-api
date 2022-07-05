using money_manager_api.Entities;

namespace money_manager_api.Services
{
    public interface IPaymentTypeService
    {
        IQueryable<PaymentType> GetAll();

        Task<PaymentType> GetByIdAsync(int id);

        Task<PaymentType> CreateAsync(PaymentType paymentType);

        Task<PaymentType> PatchAsync(int id, PaymentType paymentType);

        Task<bool> DeleteAsync(int id);
    }
}
