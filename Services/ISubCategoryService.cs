using money_manager_api.Entities;

namespace money_manager_api.Services
{
    public interface ISubCategoryService
    {
        IQueryable<SubCategory> GetAll();

        Task<SubCategory> GetByIdAsync(Guid id);

        Task<SubCategory> CreateAsync(SubCategory subCategory);

        Task<SubCategory> PatchAsync(Guid id, SubCategory subCategory);

        Task<bool> DeleteAsync(Guid id);
    }
}
