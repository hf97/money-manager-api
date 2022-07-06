using money_manager_api.Entities;

namespace money_manager_api.Services
{
    public interface IAttachmentService
    {
        IQueryable<Attachment> GetAll();

        Task<Attachment> GetByIdAsync(Guid id);

        Task<Attachment> CreateAsync(Attachment attachment);

        Task<bool> DeleteAsync(Guid id);
    }
}
