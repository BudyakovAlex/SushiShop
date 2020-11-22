using System;
using System.Globalization;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Models.Promotions;
using SushiShop.Core.Managers.Promotions;
using SushiShop.Core.Providers;
using SushiShop.Core.Resources;

namespace SushiShop.Core.ViewModels.Promotions
{
    public class PromotionDetailsViewModel : BasePageViewModel<long>
    {
        private static readonly CultureInfo CultureInfo = CultureInfo.GetCultureInfo("ru");

        private readonly IPromotionsManager promotionsManager;
        private readonly IUserSession userSession;

        private long id;
        private Promotion? promotion;

        public PromotionDetailsViewModel(IPromotionsManager promotionsManager, IUserSession userSession)
        {
            this.promotionsManager = promotionsManager;
            this.userSession = userSession;
        }

        public string ImageUrl => promotion?.RectangularImageInfo.JpgUrl ?? string.Empty;

        public string Content => promotion?.Content ?? string.Empty;

        public string IntroText => promotion?.IntroText ?? string.Empty;

        public string DateString
        {
            get
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
                            startDate.Day.ToString(),
                            GetFullDateString(endDate),
                            AppStrings.FromToDateFormat);
                    }
                    else
                    {
                        return string.Format(
                          startDate.ToString("d MMMM", CultureInfo),
                          GetFullDateString(endDate),
                          AppStrings.FromToDateFormat);
                    }
                }
                else
                {
                    return string.Format(
                        GetFullDateString(startDate),
                        GetFullDateString(endDate),
                        AppStrings.FromToDateFormat);
                }
            }
        }

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

        private string GetFullDateString(DateTime dateTime) =>
            dateTime.ToString("d MMMM yyyy", CultureInfo);
    }
}
