﻿using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Cart;
using SushiShop.Core.Data.Models.Products;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.Extensions;
using SushiShop.Core.Factories.Cart;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.Managers.Cities;
using SushiShop.Core.Messages;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Plugins;
using SushiShop.Core.Providers;
using SushiShop.Core.Providers.UserOrderPreferences;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Cart.Items;
using SushiShop.Core.ViewModels.Orders;
using SushiShop.Core.ViewModels.ProductDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Cart
{
    public class CartViewModel : BasePageViewModel
    {
        private readonly ICartManager cartManager;
        private readonly IUserSession userSession;
        private readonly ICartItemsViewModelsFactory viewModelsFactory;
        private readonly IDialog dialog;
        private readonly IUserDialogs userDialogs;
        private readonly ICitiesManager citiesManager;
        private readonly IUserOrderPreferencesProvider userOrderPreferencesProvider;

        private Topping[]? sauses;
        private Data.Models.Cart.Cart? cart;

        private string? city;
        private bool shouldShowWarningMessage;

        public CartViewModel(
            ICartManager cartManager,
            IUserSession userSession,
            ICartItemsViewModelsFactory viewModelsFactory,
            IDialog dialog,
            ICitiesManager citiesManager,
            IUserOrderPreferencesProvider userOrderPreferencesProvider)
        {
            this.cartManager = cartManager;
            this.userSession = userSession;
            this.viewModelsFactory = viewModelsFactory;
            this.dialog = dialog;
            this.userDialogs = UserDialogs.Instance;
            this.citiesManager = citiesManager;
            this.userOrderPreferencesProvider = userOrderPreferencesProvider;

            Products = new MvxObservableCollection<CartProductItemViewModel>();
            Sauces = new MvxObservableCollection<CartToppingItemViewModel>();
            Packages = new MvxObservableCollection<CartPackItemViewModel>();

            CheckoutCommand = new SafeAsyncCommand(ExecutionStateWrapper, CheckoutAsync);
            AddSaucesCommand = new SafeAsyncCommand(ExecutionStateWrapper, AddSaucesAsync);
            ApplyPromocodeCommand = new SafeAsyncCommand(ExecutionStateWrapper, ApplyPromocodeAsync);
            ShowWarningCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowWarningAsync);

            Messenger.Subscribe<RefreshCartMessage>(OnCartChanged).DisposeWith(Disposables);
            Messenger.Subscribe<CartProductChangedMessage>(OnCartProductChanged).DisposeWith(Disposables);
            Messenger.Subscribe<CityChangedMessage>(OnCityChnaged).DisposeWith(Disposables);
        }

        public IMvxCommand AddSaucesCommand { get; }
        public IMvxCommand CheckoutCommand { get; }
        public IMvxCommand ApplyPromocodeCommand { get; }
        public IMvxCommand ShowWarningCommand { get; }

        public MvxObservableCollection<CartProductItemViewModel> Products { get; }

        public MvxObservableCollection<CartToppingItemViewModel> Sauces { get; }

        public MvxObservableCollection<CartPackItemViewModel> Packages { get; }

        public string Title => AppStrings.Basket;

        public long CountProductsInCart => cart?.TotalCount ?? 0;

        public string TotalPrice => GetTotalPrice();

        public bool IsEmptyBasket => cart?.Products?.Length == 0;

        public bool CanAddSauses { get; private set; } = true;

        public bool CanAddPackages { get; private set; } = true;

        public string? PromocodeDescription => $"{AppStrings.Promocode} {cart?.Promocode?.Code}";

        public string? PromocodePrice => GetPromocodePrice();

        private string promocode = string.Empty;
        public string Promocode
        {
            get => promocode;
            set => SetProperty(ref promocode, value, () => IsValidPromocode = true);
        }

        private bool isValidPromocode = true;
        public bool IsValidPromocode
        {
            get => isValidPromocode;
            set => SetProperty(ref isValidPromocode, value);
        }

        public bool IsPromocodeApplyed => cart?.Promocode?.DiscountPercent != null;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            _ = ExecutionStateWrapper.WrapAsync(RefreshDataAsync, awaitWhenBusy: true);
        }

        protected override async Task<bool> TryToReloadDataWithConnectionAsync()
        {
            var wasConnectionBeforeLoad = HasConnection;
            await RefreshDataAsync();
            return wasConnectionBeforeLoad && HasConnection;
        }

        protected override async Task RefreshDataAsync()
        {
            city = userSession.GetCity()?.Name;

            var getBasket = cartManager.GetCartAsync(city);
            var packagingCart = cartManager.GetCartPackagingAsync(city);
            var getSauces = cartManager.GetSaucesAsync(city);

            await Task.WhenAll(getBasket, packagingCart, getSauces);

            sauses = getSauces.Result.Data;
            CanAddSauses = sauses?.Length > 0;
            await RaisePropertyChanged(nameof(CanAddSauses));

            cart = getBasket.Result.Data;
            if (cart is null)
            {
                return;
            }

            Promocode = cart!.Promocode?.Code ?? string.Empty;

            var availablePackages = packagingCart.Result.Data.Select(ProducePackageFromProduct).ToArray();
            CanAddPackages = availablePackages.Length > 0;
            await RaisePropertyChanged(nameof(CanAddPackages));

            var mergedPackages = cart!.Products.Where(product => product.Type == ProductType.Pack)
                                               .Union(availablePackages)
                                               .Select(product => viewModelsFactory.ProduceProductItemViewModel(cartManager, product, cart.Currency, cart.City, RefreshSausesCountState))
                                               .DistinctBy(package => package.Id)
                                               .ToArray();

            var allProducts = cart!.Products.Where(product => product.Type != ProductType.Pack)
                                            .Select(product => viewModelsFactory.ProduceProductItemViewModel(cartManager, product, cart.Currency, cart.City, RefreshSausesCountState))
                                            .ToArray();

            var mainProducts = allProducts.OfType<CartProductItemViewModel>().ToList();
            var toppings = allProducts.OfType<CartToppingItemViewModel>().ToList();
            var packages = mergedPackages.OfType<CartPackItemViewModel>().ToList();

            //TODO: check how it works with UI if we will have blicks replace with SwitchTo method
            Products.ReplaceWith(mainProducts);
            Sauces.ReplaceWith(toppings);
            Packages.ReplaceWith(packages);

            _ = RaisePropertyChanged(nameof(CountProductsInCart));
            _ = RaisePropertyChanged(nameof(TotalPrice));
            _ = RaisePropertyChanged(nameof(IsEmptyBasket));
            _ = RaisePropertyChanged(nameof(PromocodeDescription));
            _ = RaisePropertyChanged(nameof(PromocodePrice));
            _ = RaisePropertyChanged(nameof(IsPromocodeApplyed));
        }

        private string GetTotalPrice()
        {
            var totalPrice = cart is null ? 0 : cart.TotalSum - cart.Discount;
            return $"{totalPrice} {cart?.Currency!.Symbol}";
        }

        private void RefreshSausesCountState(int count, long sauseId)
        {
            var sause = sauses.FirstOrDefault(item => item.Id == sauseId);
            if (sause is null)
            {
                return;
            }

            sause.CountInBasket = count;
        }

        private static CartProduct ProducePackageFromProduct(Product product)
        {
            return new CartProduct(
                product.Id,
                product.CountInBasket,
                product.Price,
                0,
                product.Params?.Weight,
                null,
                0,
                product.PageTitle,
                product.Uid,
                false,
                ProductType.Pack,
                Array.Empty<CartTopping>(),
                product.MainImageInfo);
        }

        private void OnCartChanged(RefreshCartMessage message)
        {
            if (message.Sender == this)
            {
                return;
            }

            _ = SafeExecutionWrapper.WrapAsync(RefreshDataAsync);
        }

        private async Task AddSaucesAsync()
        {
            var navigationParams = new ToppingNavigationParameters(sauses.ToList(), AppStrings.AddSauce, cart!.Currency?.Symbol);
            var toppings = await NavigationManager.NavigateAsync<ToppingsViewModel, ToppingNavigationParameters, List<Topping>>(navigationParams);
            if (toppings is null)
            {
                return;
            }

            var selectedToppings = toppings.Where(topping => topping.CountInBasket > 0).ToList();
            await Task.WhenAll(ProduceAddToppingsTasks(selectedToppings));
            await RefreshDataAsync();
            Messenger.Publish(new RefreshCartMessage(this));

            IEnumerable<Task<Response<Product?>>> ProduceAddToppingsTasks(List<Topping> toppings)
            {
                foreach (var topping in toppings)
                {
                    var existingTopping = Sauces.FirstOrDefault(sauce => sauce.Id == topping.Id);
                    var count = topping.CountInBasket - (existingTopping?.CountInBasket ?? 0);
                    yield return cartManager.UpdateProductInCartAsync(city, topping.Id, existingTopping?.Uid, count, Array.Empty<Topping>());
                }
            }
        }

        private async Task ApplyPromocodeAsync()
        {
            if (Promocode.IsNullOrEmpty() && !IsPromocodeApplyed)
            {
                return;
            }

            var response = await cartManager.GetCartPromocodeAsync(city, Promocode);
            if (response.Data is null)
            {
                IsValidPromocode = false;
                var error = response.Errors.FirstOrDefault();
                if (error.IsNullOrEmpty())
                {
                    return;
                }

                await dialog.ShowToastAsync(error);
                return;
            }

            await userDialogs.AlertAsync(AppStrings.PromocodeApplied);
            await RefreshDataAsync();
        }

        private void OnCartProductChanged(CartProductChangedMessage message)
        {
            switch (message.ProductChangeAction)
            {
                case ProductChangeAction.Add:
                    cart!.TotalSum = cart!.TotalSum + message.CartProduct.Price;
                    break;
                case ProductChangeAction.Remove:
                    cart!.TotalSum = cart!.TotalSum - message.CartProduct.Price;
                    break;
            }

            RaisePropertyChanged(nameof(TotalPrice));
        }

        private async Task CheckoutAsync()
        {
            var response = await citiesManager.GetAvailableReceiveMethodsAsync();
            if (response.Data is null)
            {
                userOrderPreferencesProvider.ClearAll();
                var error = response.Errors.FirstOrDefault();
                if (error.IsNullOrEmpty())
                {
                    return;
                }

                await dialog.ShowToastAsync(error);
                return;
            }

            if (!response.Data.CanDelivery)
            {
                userOrderPreferencesProvider.ClearDeliveryData();
            }

            if (!response.Data.CanPickup)
            {
                userOrderPreferencesProvider.ClearPickupData();
            }

            var parameter = new OrderRegistrationNavigationParameter(cart!, response.Data!);
            var shouldRefresh = await NavigationManager.NavigateAsync<OrderRegistrationViewModel, OrderRegistrationNavigationParameter, bool>(parameter);
            if (shouldRefresh)
            {
                return;
            }

            await RefreshDataAsync();
        }

        private void OnCityChnaged(CityChangedMessage obj) =>
            shouldShowWarningMessage = true;

        private Task ShowWarningAsync()
        {
            if (!shouldShowWarningMessage ||
                (cart?.WarningMessage.IsNullOrEmpty() ?? false))
            {
                return Task.CompletedTask;
            }

            shouldShowWarningMessage = false;
            return userDialogs.AlertAsync(cart!.WarningMessage);
        }

        private string? GetPromocodePrice()
        {
            if (cart?.Promocode?.DiscountFixed != null && cart?.Promocode?.DiscountFixed != 0)
            {
                return $"-{cart?.Promocode?.DiscountFixed} {cart?.Currency?.Symbol}";
            }

            if (cart?.Promocode?.DiscountPercent != null && cart?.Promocode?.DiscountPercent != 0)
            {
                return $"-{cart?.Promocode?.DiscountPercent} %";
            }

            return null;
        }
    }
}