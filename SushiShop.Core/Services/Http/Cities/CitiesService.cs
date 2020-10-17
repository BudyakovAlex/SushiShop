using Newtonsoft.Json;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Dtos.City;
using SushiShop.Core.Data.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Services.Http.Cities
{
    public class CitiesService : ICitiesService
    {
        private readonly IHttpService httpService;

        public CitiesService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        //TODO: refactor it
        public async Task<RawResponse<CityDto[]>> GetCitiesAsync()
        {
            var response = await httpService.PostAsync(Constants.Rest.CitiesResource, CancellationToken.None);
            var rowResponse = JsonConvert.DeserializeObject<RawResponse<CityDto[]>>(response.Data);
            return rowResponse;
        }
    }
}
