using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Mappers.Cities;
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
    }
}
