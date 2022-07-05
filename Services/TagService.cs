using Microsoft.EntityFrameworkCore;
using money_manager_api.Entities;
using money_manager_api.Helpers;

namespace money_manager_api.Services
{
    public class TagService : ITagService
    {
        private readonly DataContext _context;

        public TagService(DataContext context)
        {
            _context = context;
        }

        public IQueryable<Tag> GetAll()
        {
            return _context.Tags;
        }

        public async Task<Tag> GetByIdAsync(Guid id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public async Task<Tag> CreateAsync(Tag tag)
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();

            if (!TagExists(tag.Id))
                throw new KeyNotFoundException();
            else
                return tag;
        }

        public async Task<Tag> PatchAsync(Guid id, Tag patch)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
                throw new KeyNotFoundException();

            _context.Entry(tag).Property(p => p.Id).IsModified = false;
            _context.Entry(tag).Property(p => p.CreatedBy).IsModified = false;
            _context.Entry(tag).Property(p => p.CreatedAt).IsModified = false;
            _context.Entry(tag).Property(p => p.ModifiedBy).IsModified = false;
            _context.Entry(tag).Property(p => p.ModifiedAt).IsModified = false;

            tag.Emoji = patch.Emoji;
            tag.Name = patch.Name;

            //_context.Tags.Update(Tag);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
                    throw new KeyNotFoundException();
                else
                    throw;
            }

            return tag;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
                throw new KeyNotFoundException();

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool TagExists(Guid id)
        {
            return _context.Tags.Any(p => p.Id == id);
        }
    }
}
