using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Cart;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.Mappers;
using SushiShop.Core.Messages;
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
        private readonly IUserDialogs userDialogs;

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
            this.userDialogs = UserDialogs.Instance;
            this.currency = currency;
            this.city = city;

            Product = product;
            StepperViewModel = new StepperViewModel(product.Count, OnCountChangedAsync);
        }

        public string? Title => Product.PageTitle;

        public string Price => $"{Product!.TotalPrice} {currency?.Symbol}";

        public long Id => Product.Id;

        public Guid? Uid => Product.Uid;

        public string? OldPrice => Product.OldPrice == 0 ? null : $"{Product!.OldPrice} {currency?.Symbol}";

        public string? ImageUrl => Product.ImageInfo?.JpgUrl;

        public string Value => GetValue();

        public int CountInBasket => Product.Count;

        public bool IsReadOnly => Product.IsReadOnly;

        public StepperViewModel StepperViewModel { get; }

        protected CartProduct Product { get; }

        protected async Task OnCountChangedAsync(int previousCount, int newCount)
        {
            var step = newCount - previousCount;
            if (newCount == 0)
            {
                //HACK: to avoid design issue
                var shouldDelete = await userDialogs.ConfirmAsync(string.Empty, AppStrings.AnswerDeleteItemFromBasket, cancelText: AppStrings.Yes, okText: AppStrings.No);
                if (shouldDelete)
                {
                    StepperViewModel.Count = previousCount;
                    return;
                }
            }

            var selectedToppings = Product.Toppings.Select(topping => topping.ToProductTopping()).ToArray() ?? Array.Empty<Topping>();
            var response = await cartManager.UpdateProductInCartAsync(city, Id, Uid, step, selectedToppings);
            if (response.Data is null)
            {
                StepperViewModel.Count = previousCount;
                return;
            }

            Product!.Uid = response.Data.Uid;
            Product!.Count = response.Data.CountInBasket;

            var isCountIncremented = newCount > previousCount;
            Product!.TotalPrice = isCountIncremented
                ? Product!.TotalPrice + Product.Price
                : Product!.TotalPrice - Product.Price;

            await Task.WhenAll(RaisePropertyChanged(nameof(CountInBasket)), RaisePropertyChanged(nameof(Price)));

            if (newCount == 0 || response.Data.IsRefreshNeeded)
            {
                Messenger.Publish(new RefreshCartMessage(this));
                return;
            }

            var action = isCountIncremented ? ProductChangeAction.Add : ProductChangeAction.Remove;
            Messenger.Publish(new CartProductChangedMessage(this, action, Product));
        }

        private string GetValue()
        {
            if (Product.Weight != null)
            {
                return Product.Weight;
            }

            if (Product.Volume != null)
            {
                return Product.Volume;
            }

            return string.Empty;
        }
    }
}