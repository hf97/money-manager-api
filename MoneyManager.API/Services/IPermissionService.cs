using money_manager_api.Entities;

namespace money_manager_api.Services
{
    public interface IPermissionService
    {
        IQueryable<Permission> GetAll();

        Task<Permission> GetByIdAsync(int id);

        Task<Permission> CreateAsync(Permission permission);

        Task<Permission> PatchAsync(int id, Permission permission);

        Task<bool> DeleteAsync(int id);
    }
}
