using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Promotions;
using SushiShop.Core.Mappers;
using SushiShop.Core.Services.Http.Promotions;

namespace SushiShop.Core.Managers.Promotions
{
    public class PromotionsManager : IPromotionsManager
    {
        private readonly IPromotionsService promotionsService;

        public PromotionsManager(IPromotionsService promotionsService)
        {
            this.promotionsService = promotionsService;
        }

        public async Task<Response<Promotion[]>> GetPromotionsAsync(string? city)
        {
            var response = await promotionsService.GetPromotionsAsync(city, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData!.Values.Select(x => x.Map()).ToArray();
                return new Response<Promotion[]>(isSuccessful: true, data);
            }

            return new Response<Promotion[]>(isSuccessful: false, new Promotion[0]);
        }

        public async Task<Response<Promotion?>> GetPromotionAsync(string? city, long id, Guid cartId)
        {
            var response = await promotionsService.GetPromotionAsync(city, id, cartId, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData!.Map();
                return new Response<Promotion?>(isSuccessful: true, data);
            }

            return new Response<Promotion?>(isSuccessful: false, default);
        }
    }
}
