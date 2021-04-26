using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Shops;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.Shops
{
    public interface IShopsManager
    {
        Task<Response<Shop[]>> GetShopsAsync(string? city, bool shouldCheckHasPizzaInShop);

        Task<Response<Dictionary<string, MetroShop[]>>> GetMetroShopsAsync(string? city, bool shouldCheckHasPizzaInShop);

        Task<Response<Shop?>> GetShopAsync(long id, string? city);
    }
}