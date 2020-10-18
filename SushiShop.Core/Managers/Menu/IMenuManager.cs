using System.Threading.Tasks;
using SushiShop.Core.Data.Http;

namespace SushiShop.Core.Managers.Menu
{
    public interface IMenuManager
    {
        Task<Response<Data.Models.Menu.Menu>> GetMenuAsync(string? city);
    }
}
