using BakeryShop.App.Components.Pages;
using BakeryShop.App.Services.Interfaces;
using BakeryShop.Shared;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

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
            var employeeJson = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8,"application/json");

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

        public async Task<IEnumerable<Employee>> GetAllEmployes()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>(await _httpClient.GetStreamAsync($"api/employee"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Employee> GetEmployeeDetails(int employeeId)
        {
            return await JsonSerializer.DeserializeAsync<Employee>(await _httpClient.GetStreamAsync($"api/employee/{employeeId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateEmployee(Employee employee)
        {
            var employeeJson = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");
            var teste = await employeeJson.ReadAsStringAsync();
            var response = await _httpClient.PutAsync("api/employee", employeeJson);

            // Verificando a resposta do servidor
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Erro: " + response.StatusCode);
                Console.WriteLine("Detalhes do Erro: " + errorContent);
                Console.WriteLine("JSON Enviado: " + await employeeJson.ReadAsStringAsync());

            }

        }
    }
}
