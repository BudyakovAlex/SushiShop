using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Orders;
using SushiShop.Core.Data.Models.Pagination;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.Orders
{
    public interface IOrdersManager
    {
        Task<Response<PaginationContainer<Order>>> GetMyOrdersAsync(int skip, int take);

        Task<Response<Order?>> GetOrderAsync(long id);

        Task<Response<Data.Models.Cart.Cart?>> RepeatOrderAsync(long id, string? city);
    }
}