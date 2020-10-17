using SushiShop.Core.Data.Dtos.City;
using SushiShop.Core.Data.Http;
using System.Threading.Tasks;

namespace SushiShop.Core.Services.Http.Cities
{
    public interface ICitiesService
    {
        Task<RawResponse<CityDto[]>> GetCitiesAsync();
    }
}