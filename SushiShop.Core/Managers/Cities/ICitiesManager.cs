using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Managers.Cities
{
    public interface ICitiesManager
    {
        Task<Response<City[]>> GetCitiesAsync();

        Task<Response<AddressSuggestion[]>> SearchAddressAsync(string? city, string query, CancellationToken token);

        Task<Response<AddressSuggestion[]>> SearchByLocationAsync(Coordinates coordinates, CancellationToken token);

        Task<Response<AvailableReceiveMethods?>> GetAvailableReceiveMethodsAsync();
    }
}