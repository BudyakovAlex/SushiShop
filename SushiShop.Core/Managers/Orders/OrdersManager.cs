using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Orders;
using SushiShop.Core.Data.Models.Pagination;
using SushiShop.Core.Mappers;
using SushiShop.Core.Providers;
using SushiShop.Core.Services.Http.Orders;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.Orders
{
    public class OrdersManager : IOrdersManager
    {
        private readonly IOrdersService ordersService;
        private readonly IUserSession userSession;

        public OrdersManager(IOrdersService ordersService, IUserSession userSession)
        {
            this.ordersService = ordersService;
            this.userSession = userSession;
        }

        public async Task<Response<PaginationContainer<Order>>> GetMyOrdersAsync(int skip, int take)
        {
            var response = await ordersService.GetMyOrdersAsync(skip, take, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData!.Map(order => order.Map());
                return new Response<PaginationContainer<Order>>(isSuccessful: true, data);
            }

            return new Response<PaginationContainer<Order>>(isSuccessful: false, PaginationContainer<Order>.Empty());
        }

        public async Task<Response<Order?>> GetOrderAsync(long id)
        {
            var response = await ordersService.GetOrderAsync(id, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData!.Map();
                return new Response<Order?>(isSuccessful: true, data);
            }

            return new Response<Order?>(isSuccessful: false, null);
        }

        public async Task<Response<Data.Models.Cart.Cart?>> RepeatOrderAsync(long id, string? city)
        {
            var response = await ordersService.RepeatOrderAsync(id, userSession.GetCartId(), city, CancellationToken.None);
            if (!response.IsSuccessful)
            {
                return new Response<Data.Models.Cart.Cart?>(isSuccessful: false, null);
            }

            var data = response.Data!.SuccessData?.Map();
            return new Response<Data.Models.Cart.Cart?>(isSuccessful: true, data);
        }
    }
}