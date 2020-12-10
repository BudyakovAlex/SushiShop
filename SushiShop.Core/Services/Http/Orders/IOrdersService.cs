using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Dtos.Orders;
using SushiShop.Core.Data.Dtos.Pagination;
using SushiShop.Core.Data.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Services.Http.Orders
{
    public interface IOrdersService
    {
        Task<HttpResponse<ResponseDto<PaginationContainerDto<OrderDto>>>> GetMyOrdersAsync(
            int skip,
            int take,
            CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<OrderDto>>> GetOrderAsync(long id, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<CartDto>>> RepeatOrderAsync(long id, Guid basketId, string? city, CancellationToken cancellationToken);
    }
}