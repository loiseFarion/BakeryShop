using BakeryShop.App.Services.Interfaces;
using BakeryShop.Shared;
using Microsoft.AspNetCore.Components;

namespace BakeryShop.App.Components.Pages
{
    public partial class EmployeeOverview
    {
        public IEnumerable<Employee> Employees { get; set; }

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }
        private List<Country> Countries { get; set; }

        private List<JobCategory> JobCategories { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Employees = (await EmployeeDataService.GetAllEmployes()).ToList();
        }       
    }
}
