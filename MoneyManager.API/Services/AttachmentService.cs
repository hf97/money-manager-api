using money_manager_api.Entities;
using money_manager_api.Helpers;

namespace money_manager_api.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly DataContext _context;

        public AttachmentService(DataContext context)
        {
            _context = context;
        }

        public IQueryable<Attachment> GetAll()
        {
            return _context.Attachments;
        }

        public async Task<Attachment> GetByIdAsync(Guid id)
        {
            return await _context.Attachments.FindAsync(id);
        }

        public async Task<Attachment> CreateAsync(Attachment attachment)
        {
            _context.Attachments.Add(attachment);
            await _context.SaveChangesAsync();

            if (!AttachmentExists(attachment.Id))
                throw new KeyNotFoundException();
            else
                return attachment;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var attachment = await _context.Attachments.FindAsync(id);

            if (attachment == null)
                throw new KeyNotFoundException();

            _context.Attachments.Remove(attachment);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool AttachmentExists(Guid id)
        {
            return _context.Attachments.Any(a => a.Id == id);
        }
    }
}
