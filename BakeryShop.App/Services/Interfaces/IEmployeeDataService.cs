using BakeryShop.Shared;

namespace BakeryShop.App.Services.Interfaces
{
    public interface IEmployeeDataService
    {
        Task<IEnumerable<Employee>> GetAllEmployes();
        Task<Employee> GetEmployeeDetails(int employeeId);
        Task<Employee> AddEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task DeleteEmployee(int employeeId);
    }
}
