using money_manager_api.Entities;

namespace money_manager_api.Services
{
    public interface IParameterService
    {
        IQueryable<Parameter> GetAll();

        Task<Parameter> GetByIdAsync(Guid id);

        Task<Parameter> GetByKeyAsync(string key);

        Task<Parameter> CreateAsync(Parameter parameter);

        Task<Parameter> PatchAsync(Guid id, Parameter parameter);

        Task<bool> DeleteAsync(Guid id);
    }
}
