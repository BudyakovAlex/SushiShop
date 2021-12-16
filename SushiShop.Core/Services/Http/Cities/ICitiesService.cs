using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Dtos.Cities;
using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Http;

namespace SushiShop.Core.Services.Http.Cities
{
    public interface ICitiesService
    {
        Task<HttpResponse<ResponseDto<CityDto[]>>> GetCitiesAsync(CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<AddressSuggestionDto[]>>> SearchAddressAsync(string? city, string query, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<AddressSuggestionDto[]>>> SearchByCoordinatesAsync(CoordinatesDto coordinates, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<AvailableReceiveMethodsDto>>> GetAvailableReceiveMethodsAsync(string? city, CancellationToken cancellationToken);
    }
}