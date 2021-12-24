﻿using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Products;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.Managers.Products;
using SushiShop.Core.Messages;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Common;
using SushiShop.Core.ViewModels.Menu.Items;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.ProductDetails
{
    public class ProductDetailsViewModel : BasePageViewModel<CardProductNavigationParameters, bool>
    {
        private readonly IProductsManager productsManager;
        private readonly ICartManager cartManager;

        private Product? product;
        private List<Topping> toppings;

        private long id;
        private string? city;
        private bool hasChanged;
        private bool isCartProduct;

        public ProductDetailsViewModel(IProductsManager productsManager, ICartManager cartManager)
        {
            this.productsManager = productsManager;
            this.cartManager = cartManager;

            toppings = new List<Topping>();

            StepperViewModel = new StepperViewModel(0, OnCountChangedAsync);
            RelatedItems = new MvxObservableCollection<ProductItemViewModel>();
            AddToCartCommand = new SafeAsyncCommand(ExecutionStateWrapper, AddToCartAsync);

            Messenger.Subscribe<OrderCreatedMessage>(OnOrderCreated).DisposeWith(Disposables);
            Messenger.Subscribe<RefreshProductsMessage>(OnCartChanged).DisposeWith(Disposables);
            Messenger.Subscribe<CartProductChangedMessage>(OnCartProductChanged).DisposeWith(Disposables);
        }

        public IMvxCommand AddToCartCommand { get; }

        public bool IsReadOnly { get; private set; }

        public string? BackgroungImageUrl => product?.MainImageInfo?.JpgUrl;

        public string Protein => product?.Params?.Proteins ?? string.Empty;

        public string Fats => product?.Params?.Fats ?? string.Empty;

        public string Carbohydrates => product?.Params?.Carbons ?? string.Empty;

        public string Calories => product?.Params?.CalorificValue ?? string.Empty;

        public string Title => product?.PageTitle ?? string.Empty;

        public string Description => product?.IntroText ?? string.Empty;

        public string Weight => product?.Params?.Weight ?? string.Empty;

        public string Price => product is null ? string.Empty : $"{product.Price} {product.Currency.Symbol}";

        public string? OldPrice => product is null || product.OldPrice == 0
            ? string.Empty
            : $"{product.OldPrice} {product.Currency.Symbol}";

        public StepperViewModel StepperViewModel { get; }

        public MvxObservableCollection<ProductItemViewModel> RelatedItems { get; }

        private bool isHiddenStepper = true;
        public bool IsHiddenStepper
        {
            get => isHiddenStepper;
            set => SetProperty(ref isHiddenStepper, value, () => RaisePropertyChanged(nameof(IsStepperVisible)));
        }

        public bool IsStepperVisible => !IsHiddenStepper;

        protected override bool DefaultResult => hasChanged;

        public override void Prepare(CardProductNavigationParameters parameter)
        {
            id = parameter.Id;
            city = parameter.City;
            IsReadOnly = parameter.IsReadonly;
            isCartProduct = parameter.IsCartProduct;
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var getProductTask = productsManager.GetProductAsync(id, city);
            var getRelatedProductTask = productsManager.GetRelatedProductsAsync(id, city);

            await Task.WhenAll(getProductTask, getRelatedProductTask);

            product = getProductTask.Result.Data;
            var relatedProducts = getRelatedProductTask.Result.Data.ToList();
            toppings = product?.Params?.AvailableToppings?.ToList() ?? new List<Topping>();
            StepperViewModel.Count = product?.CountInBasket ?? 0;
            IsHiddenStepper = StepperViewModel.Count == 0;

            var viewModels = relatedProducts.Select(product => new ProductItemViewModel(cartManager, product, city, RefreshDataAsync)).ToList();
            RelatedItems.AddRange(viewModels);
            await RaiseAllPropertiesChanged();
        }

        protected override Task RefreshDataAsync()
        {
            hasChanged = true;
            return base.RefreshDataAsync();
        }

        private void OnOrderCreated(OrderCreatedMessage obj)
        {
            StepperViewModel.Count = 0;
            IsHiddenStepper = true;
        }

        private async Task AddToCartAsync()
        {
            try
            {
                if (toppings.Count == 0)
                {
                    return;
                }

                var navigationParams = new ToppingNavigationParameters(toppings, AppStrings.MakeItTastier, product!.Currency.Symbol);
                var result = await NavigationManager.NavigateAsync<ToppingsViewModel, ToppingNavigationParameters, List<Topping>>(navigationParams);
                if (result is null)
                {
                    return;
                }

                toppings = result;
            }
            finally
            {
                StepperViewModel.AddCommand.Execute();
                IsHiddenStepper = false;
            }
        }

        private async Task OnCountChangedAsync(int previousCount, int newCount)
        {
            IsHiddenStepper = newCount == 0;
            var step = newCount - previousCount;

            var selectedToppings = toppings.Where(topping => topping.CountInBasket > 0).ToArray();
            var response = await cartManager.UpdateProductInCartAsync(city, product!.Id, product?.Uid, step, selectedToppings);
            if (response.Data is null)
            {
                return;
            }

            hasChanged = true;
            Messenger.Publish(new RefreshCartMessage(this));
            Messenger.Publish(new RefreshProductsMessage(this));

            product!.Uid = response.Data.Uid;
        }

        private void OnCartChanged(RefreshProductsMessage message)
        {
            if (message.Sender == this)
            {
                return;
            }

            _ = ExecutionStateWrapper.WrapAsync(() =>
                SafeExecutionWrapper.WrapAsync(ReloadProductDetailsAsync),
                awaitWhenBusy: true);
        }

        private void OnCartProductChanged(CartProductChangedMessage message)
        {
            if (message.CartProduct.Id == id)
            {
                _ = ExecutionStateWrapper.WrapAsync(() =>
                SafeExecutionWrapper.WrapAsync(ReloadProductDetailsAsync),
                awaitWhenBusy: true);
            }
        }

        private async Task ReloadProductDetailsAsync()
        {
            var response = await productsManager.GetProductAsync(id, city);
            if (!response.IsSuccessful || response.Data?.CountInBasket > 0)
            {
                if (response.Data?.CountInBasket != null)
                {
                    StepperViewModel.Count = response.Data!.CountInBasket!;
                }

                return;
            }

            StepperViewModel.Count = 0;
            IsHiddenStepper = true;

            product = response.Data;
            toppings = product?.Params?.AvailableToppings?.ToList() ?? new List<Topping>();

            await RaiseAllPropertiesChanged();
        }
    }
}