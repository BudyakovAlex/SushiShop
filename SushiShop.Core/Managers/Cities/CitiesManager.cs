using SushiShop.Core.Data.Models;
using SushiShop.Core.Mappers;
using SushiShop.Core.Services.Http.Cities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.Cities
{
    public class CitiesManager : ICitiesManager
    {
        private readonly ICitiesService citiesService;

        public CitiesManager(ICitiesService citiesService)
        {
            this.citiesService = citiesService;
        }

        public async Task<City[]> GetCitiesAsync()
        {
            var response = await citiesService.GetCitiesAsync();
            var models = response?.Data?.Select(dto => dto.Map())?.ToArray() ?? Array.Empty<City>();
            return models;
        }
    }
}
