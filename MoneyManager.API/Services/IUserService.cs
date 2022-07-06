using money_manager_api.Entities;

namespace money_manager_api.Services
{
    public interface IUserService
    {
        IQueryable<User> GetAll();

        Task<User> GetByIdAsync(Guid id);

        Task<User> CreateAsync(User user);

        Task<User> PatchAsync(Guid id, User user);

        Task<bool> DeleteAsync(Guid id);
    }
}
