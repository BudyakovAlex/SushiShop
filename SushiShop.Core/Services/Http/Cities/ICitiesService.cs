using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Dtos.City;
using SushiShop.Core.Data.Http;

namespace SushiShop.Core.Services.Http.Cities
{
    public interface ICitiesService
    {
        Task<HttpResponse<ResponseDto<CityDto[]>>> GetCitiesAsync(CancellationToken cancellationToken);
    }
}