using BakeryShop.App.Services.Interfaces;
using BakeryShop.Shared;
using Microsoft.AspNetCore.Components;

namespace BakeryShop.App.Components.Pages
{
    public partial class EmployeeEdit
    {
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }
        [Inject]
        public ICountryDataService CountryDataService { get; set; }
        [Inject]
        public IJobCategoryDataService JobCategoryDataService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }


        [Parameter]
        public string EmployeeId { get; set; }

        public Employee EmployeeForm { get; set; } = new Employee();
        public List<Country> Countries { get; set; } = new List<Country>();
        public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();

        protected string CountryId = string.Empty;
        protected string JobCategoryId = string.Empty;

        //used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;


        protected override async Task OnInitializedAsync()
        {
            Saved = false;
            Countries = (await CountryDataService.GetAllCountries()).ToList();
            //Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
            JobCategories = (await JobCategoryDataService.GetAllJobCategories()).ToList();

            int.TryParse(EmployeeId, out var employeeId);

            if (employeeId == 0) //new employee is being created
            {
                //add some defaults
                EmployeeForm = new Employee { CountryId = 1, JobCategoryId = 1, BirthDate = DateTime.Now, JoinedDate = DateTime.Now };
            }
            else
            {
                EmployeeForm = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
            }

            CountryId = EmployeeForm.CountryId.ToString();
            JobCategoryId = EmployeeForm.JobCategoryId.ToString();
        }

        protected async Task HandleValidSubmit()
        {
            Saved = false;
            EmployeeForm.CountryId = int.Parse(CountryId);
            EmployeeForm.JobCategoryId = int.Parse(JobCategoryId);

            if (EmployeeForm.EmployeeId == 0) //new
            {
                var addedEmployee = await EmployeeDataService.AddEmployee(EmployeeForm);
                if (addedEmployee != null)
                {
                    StatusClass = "alert-success";
                    Message = "New employee added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new employee. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                JobCategory nameJobCategory = await JobCategoryDataService.GetJobCategorById(EmployeeForm.JobCategoryId);
                EmployeeForm.JobCategory = nameJobCategory;

                Country nameCountry = await CountryDataService.GetCountryById(EmployeeForm.CountryId);
                EmployeeForm.Country = nameCountry;
                await EmployeeDataService.UpdateEmployee(EmployeeForm);
                StatusClass = "alert-success";
                Message = "Employee updated successfully.";
                Saved = true;
            }
        }


        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task DeleteEmployee()
        {
            await EmployeeDataService.DeleteEmployee(EmployeeForm.EmployeeId);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }

        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/employeeoverview");
        }
    }
}
