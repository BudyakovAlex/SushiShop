using SushiShop.Core.Data.Models;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.Cities
{
    public interface ICitiesManager
    {
        Task<City[]> GetCitiesAsync();
    }
}