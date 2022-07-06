using money_manager_api.Entities;

namespace money_manager_api.Services
{
    public interface ICategoryService
    {
        IQueryable<Category> GetAll();

        Task<Category> GetByIdAsync(Guid id);

        Task<Category> CreateAsync(Category category);

        Task<Category> PatchAsync(Guid id, Category category);

        Task<bool> DeleteAsync(Guid id);
    }
}
