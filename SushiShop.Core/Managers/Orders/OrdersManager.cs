using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Orders;
using SushiShop.Core.Data.Models.Pagination;
using SushiShop.Core.Mappers;
using SushiShop.Core.Providers;
using SushiShop.Core.Services.Http.Orders;
using System;
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

        public async Task<Response<OrderConfirmed?>> CreateOrderAsync(OrderRequest orderRequest)
        {
            var requestDto = orderRequest.Map();
            var response = await ordersService.CreateOrderAsync(requestDto, CancellationToken.None);
            if (response.IsSuccessful &&
                response.Data?.SuccessData != null)
            {
                var data = response.Data!.SuccessData!.Map();
                return new Response<OrderConfirmed?>(isSuccessful: true, data);
            }

            var errors = response.Data!.Errors ?? Array.Empty<string>();
            return new Response<OrderConfirmed?>(isSuccessful: false, null, errors: errors);
        }

        public async Task<Response<PaginationContainer<Order>>> GetMyOrdersAsync(int page, int take)
        {
            var response = await ordersService.GetMyOrdersAsync(page, take, CancellationToken.None);
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

        public async Task<Response<bool>> CheckOrderPaymentAsync(long id, string phone)
        {
            var response = await ordersService.CheckOrderPaymentAsync(id, phone, CancellationToken.None);

            var errors = response.Data?.Errors ?? Array.Empty<string>();
            return new Response<bool>(isSuccessful: true, errors.Length == 0, errors);
        }

        public async Task<Response<decimal>> CalculateDiscountAsync(string? phone)
        {
            var city = userSession.GetCity();
            var basketId = userSession.GetCartId();

            var response = await ordersService.CalculateDiscountAsync(phone, city!.Name, basketId, CancellationToken.None);
            if (response.IsSuccessful &&
                decimal.TryParse(response.Data!.SuccessData!.ToString(), out var discount))
            {
                return new Response<decimal>(isSuccessful: true, discount);
            }

            return new Response<decimal>(isSuccessful: false, 0);
        }
    }
}