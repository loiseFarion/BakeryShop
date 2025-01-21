using BakeryShop.App.Services.Interfaces;
using BakeryShop.Shared;
using System.Text.Json;

namespace BakeryShop.App.Services
{
    public class CountryDataService : ICountryDataService
    {
        private readonly HttpClient _httpClient;

        public CountryDataService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Country>>(await _httpClient.GetStreamAsync($"api/country"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Country> GetCountryById(int countryId)
        {
            return await JsonSerializer.DeserializeAsync<Country>(await _httpClient.GetStreamAsync($"api/country/{countryId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        }
    }
}
