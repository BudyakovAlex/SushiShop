﻿using SushiShop.Core.Common;
using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Dtos.Orders;
using SushiShop.Core.Data.Dtos.Pagination;
using SushiShop.Core.Data.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Services.Http.Orders
{
    public class OrdersService : IOrdersService
    {
        private readonly IHttpService httpService;

        public OrdersService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public Task<HttpResponse<ResponseDto<PaginationContainerDto<OrderDto>>>> GetMyOrdersAsync(
            int skip,
            int take,
            CancellationToken cancellationToken)
        {
            var body = new
            {
                offset = skip,
                limit = take
            };

            return httpService.ExecuteAsync<ResponseDto<PaginationContainerDto<OrderDto>>>(
                Method.Post,
                Constants.Rest.ProfileGetOrdersResource,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<OrderDto>>> GetOrderAsync(long id, CancellationToken cancellationToken)
        {
            var body = new
            {
                orderId = id
            };

            return httpService.ExecuteAsync<ResponseDto<OrderDto>>(
                Method.Post,
                Constants.Rest.ProfileGetOrderDetailsResource,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<CartDto>>> RepeatOrderAsync(long id, Guid basketId, string? city, CancellationToken cancellationToken)
        {
            var body = new
            {
                orderId = id,
                basketId,
                city
            };

            return httpService.ExecuteAsync<ResponseDto<CartDto>>(
                Method.Post,
                Constants.Rest.ProfileRepearOrderResource,
                body,
                cancellationToken);
        }
    }
}