using BakeryShop.Shared;

namespace BakeryShop.App.Services.Interfaces
{
    public interface ICountryDataService
    {
        Task<IEnumerable<Country>> GetAllCountries();

        Task<Country> GetCountryById(int countryId);
    }
}
