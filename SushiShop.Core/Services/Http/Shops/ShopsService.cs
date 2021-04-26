using SushiShop.Core.Common;
using SushiShop.Core.Data.Dtos.Shops;
using SushiShop.Core.Data.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Services.Http.Shops
{
    public class ShopsService : IShopsService
    {
        private readonly IHttpService httpService;

        public ShopsService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public Task<HttpResponse<ResponseDto<Dictionary<string, MetroShopDto[]>>>> GetMetroShopsAsync(
            string? city,
            bool shouldCheckHasPizzaInShop,
            CancellationToken cancellationToken)
        {
            var body = new { City = city, BasketHasPizza = shouldCheckHasPizzaInShop };
            return httpService.ExecuteAsync<ResponseDto<Dictionary<string, MetroShopDto[]>>>(
                Method.Post,
                Constants.Rest.MetroShopsResource,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<ShopDto>>> GetShopAsync(long id, string? city, CancellationToken cancellationToken)
        {
            var body = new
            {
                City = city,
                id
            };

            return httpService.ExecuteAsync<ResponseDto<ShopDto>>(
                Method.Post,
                Constants.Rest.ShopsResource,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<ShopDto[]>>> GetShopsAsync(string? city, bool shouldCheckHasPizzaInShop, CancellationToken cancellationToken)
        {
            var body = new { City = city, BasketHasPizza = shouldCheckHasPizzaInShop };
            return httpService.ExecuteAsync<ResponseDto<ShopDto[]>>(
                Method.Post,
                Constants.Rest.ShopsResource,
                body,
                cancellationToken);
        }
    }
}
