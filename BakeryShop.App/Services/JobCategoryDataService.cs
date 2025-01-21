using BakeryShop.App.Services.Interfaces;
using BakeryShop.Shared;
using System.Net.Http;
using System.Runtime;
using System.Text.Json;

namespace BakeryShop.App.Services
{
    public class JobCategoryDataService : IJobCategoryDataService
    {
        private readonly HttpClient _httpClient;

        public JobCategoryDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<JobCategory>> GetAllJobCategories()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<JobCategory>>(await _httpClient.GetStreamAsync($"api/jobcategory"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<JobCategory> GetJobCategorById(int jobcategorId)
        {
            return await JsonSerializer.DeserializeAsync<JobCategory>(await _httpClient.GetStreamAsync($"api/jobcategory/{jobcategorId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        }
    }
}
