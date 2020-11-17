using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Dtos.Promotions;
using SushiShop.Core.Data.Http;

namespace SushiShop.Core.Services.Http.Promotions
{
    public interface IPromotionsService
    {
        Task<HttpResponse<ResponseDto<Dictionary<string, PromotionDto>>>> GetPromotionsAsync(string? city, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<PromotionDto>>> GetPromotionAsync(string? city, int id, CancellationToken cancellationToken);
    }
}
