using money_manager_api.Entities;

namespace money_manager_api.Services
{
    public interface ITagService
    {
        IQueryable<Tag> GetAll();

        Task<Tag> GetByIdAsync(Guid id);

        Task<Tag> CreateAsync(Tag tag);

        Task<Tag> PatchAsync(Guid id, Tag tag);

        Task<bool> DeleteAsync(Guid id);
    }
}
