using BakeryShop.Api.Models;
using BakeryShop.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BakeryShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IJobCategoryRepository _jobCategoryRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, ICountryRepository countryRepository, IJobCategoryRepository jobCategoryRepository)
        {
            _employeeRepository = employeeRepository;
            _countryRepository = countryRepository;
            _jobCategoryRepository = jobCategoryRepository;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            return Ok(_employeeRepository.GetAllEmployees());
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            return Ok(_employeeRepository.GetEmployeeById(id));
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest();

            if (employee.FirstName == string.Empty || employee.LastName == string.Empty)
            {
                ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryId = _countryRepository.GetCountryById(employee.CountryId);
            var jobCategoryId = _jobCategoryRepository.GetJobCategoryById(employee.JobCategoryId);
            
            if (countryId != null)
            {
                employee.Country = null;
            }
            if(jobCategoryId != null)
            {
                employee.JobCategory = null;
            }
            
            
            var createdEmployee = _employeeRepository.AddEmployee(employee);

            return Created("employee", createdEmployee);
        }

        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest();

            if (employee.FirstName == string.Empty || employee.LastName == string.Empty)
            {
                ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeToUpdate = _employeeRepository.GetEmployeeById(employee.EmployeeId);

            if (employeeToUpdate == null)
                return NotFound();

            _employeeRepository.UpdateEmployee(employee);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            if (id == 0)
                return BadRequest();

            var employeeToDelete = _employeeRepository.GetEmployeeById(id);
            if (employeeToDelete == null)
                return NotFound();

            _employeeRepository.DeleteEmployee(id);

            return NoContent();//success
        }
    }
}
