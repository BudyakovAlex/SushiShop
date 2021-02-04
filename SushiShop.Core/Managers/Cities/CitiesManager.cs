using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Mappers;
using SushiShop.Core.Services.Http.Cities;

namespace SushiShop.Core.Managers.Cities
{
    public class CitiesManager : ICitiesManager
    {
        private readonly ICitiesService citiesService;

        public CitiesManager(ICitiesService citiesService)
        {
            this.citiesService = citiesService;
        }

        public async Task<Response<City[]>> GetCitiesAsync()
        {
            var response = await citiesService.GetCitiesAsync(CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData!.Select(x => x.Map()).ToArray();
                return new Response<City[]>(isSuccessful: true, data);
            }

            return new Response<City[]>(isSuccessful: false, Array.Empty<City>());
        }

        public async Task<Response<AddressSuggestion[]>> SearchAddressAsync(string? city, string query, CancellationToken token)
        {
            var response = await citiesService.SearchAddressAsync(city, query, token);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData!.Select(x => x.Map()).ToArray();
                return new Response<AddressSuggestion[]>(isSuccessful: true, data);
            }

            return new Response<AddressSuggestion[]>(isSuccessful: false, Array.Empty<AddressSuggestion>());
        }

        public async Task<Response<AddressSuggestion[]>> SearchByLocationAsync(Coordinates coordinates, CancellationToken token)
        {
            var response = await citiesService.SearchByCoordinatesAsync(coordinates.Map(), token);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData!.Select(x => x.Map()).ToArray();
                return new Response<AddressSuggestion[]>(isSuccessful: true, data);
            }

            return new Response<AddressSuggestion[]>(isSuccessful: false, Array.Empty<AddressSuggestion>());
        }
    }
}
