using System.Threading.Tasks;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Promotions;

namespace SushiShop.Core.Managers.Promotions
{
    public interface IPromotionsManager
    {
        Task<Response<Promotion[]>> GetPromotionsAsync(string? city);

        Task<Response<Promotion?>> GetPromotionAsync(string? city, int id);
    }
}
