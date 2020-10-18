using System.Threading.Tasks;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models;

namespace SushiShop.Core.Managers.Cities
{
    public interface ICitiesManager
    {
        Task<Response<City[]>> GetCitiesAsync();
    }
}