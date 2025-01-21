using BakeryShop.Shared;

namespace BakeryShop.App.Services.Interfaces
{
    public interface IJobCategoryDataService
    {
        Task<IEnumerable<JobCategory>> GetAllJobCategories();

        Task<JobCategory> GetJobCategorById(int jobcategorId);
    }
}
