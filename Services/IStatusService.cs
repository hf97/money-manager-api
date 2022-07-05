using money_manager_api.Entities;

namespace money_manager_api.Services
{
    public interface IStatusService
    {
        IQueryable<Status> GetAll();

        Task<Status> GetByIdAsync(int id);

        Task<Status> CreateAsync(Status status);

        Task<Status> PatchAsync(int id, Status status);

        Task<bool> DeleteAsync(int id);
    }
}
