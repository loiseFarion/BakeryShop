using BakeryShop.App.Components.Pages;
using BakeryShop.App.Services.Interfaces;
using BakeryShop.Shared;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BakeryShop.App.Services
{
    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly HttpClient _httpClient;

        public EmployeeDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            // Preenchendo os objetos aninhados se estiverem nulos
            if (employee.Country == null)
            {
                employee.Country = new Country
                {
                    CountryId = 0,
                    Name = "string"
                };
            }

            if (employee.JobCategory == null)
            {
                employee.JobCategory = new JobCategory
                {
                    JobCategoryId = 0,
                    JobCategoryName = "string"
                };
            }
            
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var employeeJson = new StringContent(JsonSerializer.Serialize(employee, options), Encoding.UTF8, "application/json");
            var teste = employeeJson.ReadAsStringAsync();
            var response = await _httpClient.PostAsync("api/employee",employeeJson);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var addEmployee = JsonSerializer.Deserialize<Employee>(jsonResponse);
                return addEmployee;
            }
            return null;
        }

        public async Task DeleteEmployee(int employeeId)
        {
            await _httpClient.DeleteAsync($"api/employee/{employeeId}");
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>(await _httpClient.GetStreamAsync($"api/employee"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Employee> GetEmployeeDetails(int employeeId)
        {
            return await JsonSerializer.DeserializeAsync<Employee>(await _httpClient.GetStreamAsync($"api/employee/{employeeId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateEmployee(Employee employee)
        { 
            // Preenchendo os objetos aninhados se estiverem nulos
            if (employee.Country == null)
            {
                employee.Country = new Country
                {
                    CountryId = employee.CountryId,
                    Name = employee.Country.Name // ou outro valor padrão
                };
            }

            if (employee.JobCategory == null)
            {
                employee.JobCategory = new JobCategory
                {
                    JobCategoryId = employee.JobCategoryId,
                    JobCategoryName = employee.JobCategory.JobCategoryName // ou outro valor padrão
                };
            }

            var employeeJson = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/employee", employeeJson);

            
         }
    }
}
