using System;
using System.Globalization;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Models.Promotions;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.Managers.Promotions;
using SushiShop.Core.Messages;
using SushiShop.Core.Providers;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Common;

namespace SushiShop.Core.ViewModels.Promotions
{
    public class PromotionDetailsViewModel : BasePageViewModel<long>
    {
        private static readonly CultureInfo CultureInfo = CultureInfo.GetCultureInfo("ru");

        private readonly IPromotionsManager promotionsManager;
        private readonly IUserSession userSession;
        private readonly ICartManager cartManager;

        private long id;
        private Promotion? promotion;

        public PromotionDetailsViewModel(
            IPromotionsManager promotionsManager,
            IUserSession userSession,
            ICartManager cartManager)
        {
            this.promotionsManager = promotionsManager;
            this.userSession = userSession;
            this.cartManager = cartManager;
        }

        private StepperViewModel? stepperViewModel;
        public StepperViewModel? StepperViewModel =>
            stepperViewModel ??= CreateStepperViewModelOrDefault();

        public string ImageUrl => promotion?.SquareImageInfo.JpgUrl ?? string.Empty;

        public string HtmlContent => promotion?.Content ?? string.Empty;

        public string IntroTitle => promotion?.IntroText ?? string.Empty;

        public bool CanAddToCart => promotion?.Product != null;

        public string PublicationDateRangeTitle => GetPublicationDateRangeTitle();

        public override void Prepare(long parameter)
        {
            id = parameter;
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            _ = ExecutionStateWrapper.WrapAsync(() => LoadDataAsync(), awaitWhenBusy: true);
        }

        private async Task LoadDataAsync()
        {
            var city = userSession.GetCity();
            var response = await promotionsManager.GetPromotionAsync(city?.Name, id);
            if (response.IsSuccessful)
            {
                promotion = response.Data;
                _ = RaiseAllPropertiesChanged();
            }
        }

        private string GetLongDateString(DateTime dateTime) =>
            dateTime.ToString(Constants.Format.DateTime.LongDate, CultureInfo);

        private async Task OnCountChangedAsync(int previousCount, int newCount)
        {
            var step = newCount - previousCount;

            var city = userSession.GetCity();
            var product = promotion!.Product!;
            var response = await cartManager.UpdateProductInCartAsync(city?.Name, product.Id, product.Uid, step, Array.Empty<Topping>());
            if (response.Data is null)
            {
                return;
            }

            Messenger.Publish(new RefreshCartMessage(this));
            product.Uid = response.Data.Uid;
        }

        private StepperViewModel? CreateStepperViewModelOrDefault()
        {
            var product = promotion?.Product;
            return product is null
                ? null
                : new StepperViewModel(AppStrings.Cart, product.CountInBasket, OnCountChangedAsync);
        }

        private string GetPublicationDateRangeTitle()
        {
            if (promotion is null)
            {
                return string.Empty;
            }

            var startDate = promotion.PublicationStartDate.Date;
            var endDate = promotion.PublicationEndDate.Date;
            if (startDate.Year == endDate.Year)
            {
                if (startDate.Month == endDate.Month)
                {
                    return string.Format(
                        AppStrings.FromToDateFormat,
                        startDate.Day.ToString(),
                        GetLongDateString(endDate));
                }
                else
                {
                    return string.Format(
                        AppStrings.FromToDateFormat,
                        startDate.ToString(Constants.Format.DateTime.ShortDate, CultureInfo),
                        GetLongDateString(endDate));
                }
            }
            else
            {
                return string.Format(
                    AppStrings.FromToDateFormat,
                    GetLongDateString(startDate),
                    GetLongDateString(endDate));
            }
        }
    }
}
