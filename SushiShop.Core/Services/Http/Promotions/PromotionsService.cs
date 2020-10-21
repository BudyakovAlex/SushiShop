using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Dtos.Promotions;
using SushiShop.Core.Data.Http;

namespace SushiShop.Core.Services.Http.Promotions
{
    public class PromotionsService : IPromotionsService
    {
        private readonly IHttpService httpService;

        public PromotionsService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<HttpResponse<ResponseDto<Dictionary<string, PromotionDto>>>> GetPromotionsAsync(string? city, CancellationToken cancellationToken)
        {
            return await httpService.ExecuteAsync<ResponseDto<Dictionary<string, PromotionDto>>>(
                Method.Post,
                Constants.Rest.PromotionsResource,
                cancellationToken);
        }
    }
}
