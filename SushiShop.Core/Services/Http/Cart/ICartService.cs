﻿using SushiShop.Core.Data.Dtos.Products;
using SushiShop.Core.Data.Http;
using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Dtos.Cart;

namespace SushiShop.Core.Services.Http.Cart
{
    public interface ICartService
    {
        Task<HttpResponse<ResponseDto<CartProductDto>>> UpdateProductInCartAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<CartDto>>> GetCartAsync(string сity, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<PromocodeDto>>> GetCartPromocodeAsync(string city, string promocode, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<PackagingDto[]>>> GetCartPackagingAsync(string city, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<SaucesDto[]>>> GetSaucesAsync(string city, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<CartDto>>> ClearCartAsync(string city, CancellationToken cancellationToken);
    }
}