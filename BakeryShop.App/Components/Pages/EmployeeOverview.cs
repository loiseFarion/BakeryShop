using BakeryShop.App.Components.Dialog;
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
        private AddEmployeeDialog AddEmployeeDialog { get; set; }
        private List<JobCategory> JobCategories { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
        }

        protected void QuickAddEmployee()
        {
            AddEmployeeDialog.Show();
        }

        public async void AddEmployeeDialog_OnDialogClos()
        {
            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
            StateHasChanged();
        }
    }
}