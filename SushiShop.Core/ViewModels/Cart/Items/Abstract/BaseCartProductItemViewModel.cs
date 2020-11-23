﻿using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Models.Cart;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.Mappers;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Cart.Items.Abstract
{
    public abstract class BaseCartProductItemViewModel : BaseViewModel
    {
        private readonly ICartManager cartManager;

        private readonly Currency? currency;

        private readonly string? city;

        protected BaseCartProductItemViewModel(
            ICartManager cartManager,
            CartProduct product,
            Currency? currency,
            string? city)
        {
            product.ThrowIfNull();

            this.cartManager = cartManager;
            this.currency = currency;
            this.city = city;

            Product = product;
            StepperViewModel = new StepperViewModel(product.Count, OnCountChangedAsync);
        }

        public string? Title => Product.PageTitle;

        public string Price => $"{Product!.Price} {currency?.Symbol}";

        public long Id => Product.Id;

        public Guid? Uid => Product.Uid;

        public string? OldPrice => Product.OldPrice == 0 ? null : $"{Product!.OldPrice} {currency?.Symbol}";

        public string Value => GetValue();

        public int CountInBasket => Product.Count;

        public StepperViewModel StepperViewModel { get; }

        protected CartProduct Product { get; }

        protected async Task OnCountChangedAsync(int previousCount, int newCount)
        {
            var step = newCount - previousCount;
            var selectedToppings = Product.Toppings.Select(topping => topping.ToProductTopping()).ToArray() ?? Array.Empty<Topping>();
            var response = await cartManager.UpdateProductInCartAsync(city, Product.Id, Product?.Uid, step, selectedToppings);

            if (response.Data is null)
            {
                return;
            }

            Product!.Count = response.Data.CountInBasket;
            await RaisePropertyChanged(nameof(CountInBasket));
        }

        private string GetValue()
        {
            if (Product.Weight.HasValue)
            {
                return $"{Product.Weight} {AppStrings.GramSymbol}";
            }

            if (Product.Volume.HasValue)
            {
                return $"{Product.Volume} {AppStrings.MillilitersSymbol}";
            }

            return string.Empty;
        }
    }
}