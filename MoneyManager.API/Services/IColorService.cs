using money_manager_api.Entities;

namespace money_manager_api.Services
{
    public interface IColorService
    {
        IQueryable<Color> GetAll();

        Task<Color> GetByIdAsync(Guid id);

        Task<Color> CreateAsync(Color color);

        Task<Color> PatchAsync(Guid id, Color color);

        Task<bool> DeleteAsync(Guid id);
    }
}
