using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Dtos.Cities;
using SushiShop.Core.Data.Http;

namespace SushiShop.Core.Services.Http.Cities
{
    public class CitiesService : ICitiesService
    {
        private readonly IHttpService httpService;

        public CitiesService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<HttpResponse<ResponseDto<CityDto[]>>> GetCitiesAsync(CancellationToken cancellationToken)
        {
            return await httpService.ExecuteAsync<ResponseDto<CityDto[]>>(
                Method.Post,
                Constants.Rest.CitiesResource,
                null,
                cancellationToken);
        }
    }
}
