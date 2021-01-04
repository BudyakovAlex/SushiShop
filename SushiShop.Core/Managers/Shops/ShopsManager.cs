using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Shops;
using SushiShop.Core.Mappers;
using SushiShop.Core.Services.Http.Shops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.Shops
{
    public class ShopsManager : IShopsManager
    {
        private readonly IShopsService shopsService;

        public ShopsManager(IShopsService shopsService)
        {
            this.shopsService = shopsService;
        }

        public async Task<Response<Dictionary<string, MetroShop[]>>> GetMetroShopsAsync(string? city)
        {
            var response = await shopsService.GetMetroShopsAsync(city, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.ToDictionary(kv => kv.Key, kv => kv.Value.Select(item => item.Map()).ToArray()) ?? new Dictionary<string, MetroShop[]>();
                return new Response<Dictionary<string, MetroShop[]>>(isSuccessful: true, data);
            }

            return new Response<Dictionary<string, MetroShop[]>>(isSuccessful: false, new Dictionary<string, MetroShop[]>(), response.Data?.Errors ?? Array.Empty<string>());
        }

        public async Task<Response<Shop?>> GetShopAsync(long id, string? city)
        {
            var response = await shopsService.GetShopAsync(id, city, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<Shop?>(isSuccessful: true, data);
            }

            return new Response<Shop?>(isSuccessful: false, null, response.Data?.Errors ?? Array.Empty<string>());
        }

        public async Task<Response<Shop[]>> GetShopsAsync(string? city)
        {
            var response = await shopsService.GetShopsAsync(city, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Select(metro => metro.Map()).ToArray() ?? Array.Empty<Shop>();
                return new Response<Shop[]>(isSuccessful: true, data);
            }

            return new Response<Shop[]>(isSuccessful: false, Array.Empty<Shop>(), response.Data?.Errors ?? Array.Empty<string>());
        }
    }
}