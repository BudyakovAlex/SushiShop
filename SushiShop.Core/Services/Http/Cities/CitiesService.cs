using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Dtos.Cities;
using SushiShop.Core.Data.Dtos.Common;
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

        public Task<HttpResponse<ResponseDto<AvailableReceiveMethodsDto>>> GetAvailableReceiveMethodsAsync(string? city, CancellationToken cancellationToken)
        {
            var body = new { city };
            return httpService.ExecuteAsync<ResponseDto<AvailableReceiveMethodsDto>>(
                Method.Post,
                Constants.Rest.CitiesOrderReceiveMethodsResource,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<CityDto[]>>> GetCitiesAsync(CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<CityDto[]>>(
                Method.Post,
                Constants.Rest.CitiesResource,
                null,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<AddressSuggestionDto[]>>> SearchAddressAsync(string? city, string query, CancellationToken cancellationToken)
        {
            var body = new
            {
                city,
                query
            };

            return httpService.ExecuteAsync<ResponseDto<AddressSuggestionDto[]>>(
               Method.Post,
               Constants.Rest.CitiesSearchAddressResource,
               body,
               cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<AddressSuggestionDto[]>>> SearchByCoordinatesAsync(CoordinatesDto coordinates, CancellationToken cancellationToken)
        {
            var body = new
            {
                latitude = coordinates.Latitude,
                longitude = coordinates.Longitude
            };

            return httpService.ExecuteAsync<ResponseDto<AddressSuggestionDto[]>>(
               Method.Post,
               Constants.Rest.CitiesCheckCoordinatesResource,
               body,
               cancellationToken);
        }
    }
}
