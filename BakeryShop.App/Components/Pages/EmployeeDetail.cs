using BakeryShop.App.Services;
using BakeryShop.App.Services.Interfaces;
using BakeryShop.ComponentsLibrary.Map;
using BakeryShop.Shared;
using Microsoft.AspNetCore.Components;

namespace BakeryShop.App.Components.Pages
{
    public partial class EmployeeDetail
    {
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        [Parameter]
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; } = new Employee();

        public List<Marker> MapMarkers { get; set; } = new List<Marker>();
        protected override async Task OnInitializedAsync()
        {
            Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
            MapMarkers = new List<Marker>{
                new Marker { Description = $"{Employee.FirstName} {Employee.LastName}", ShowPopup = false, X = Employee.Longitude, Y = Employee.Latitude }
            };
        }


    }
}
