using System.Linq;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.ViewModels;
using SushiShop.Core.Managers.Promotions;
using SushiShop.Core.Providers;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Promotions.Items;

namespace SushiShop.Core.ViewModels.Promotions
{
    public class PromotionsViewModel : BasePageViewModel
    {
        private readonly IPromotionsManager promotionsManager;
        private readonly IUserSession userSession;

        public PromotionsViewModel(IPromotionsManager promotionsManager, IUserSession userSession)
        {
            this.promotionsManager = promotionsManager;
            this.userSession = userSession;

            Items = new MvxObservableCollection<PromotionItemViewModel>();
        }

        public MvxObservableCollection<PromotionItemViewModel> Items { get; }

        public string Title => AppStrings.Promotions;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            _ = ExecutionStateWrapper.WrapAsync(() => LoadDataAsync(), awaitWhenBusy: true);
        }

        private async Task LoadDataAsync()
        {
            var city = userSession.GetCity();
            var response = await promotionsManager.GetPromotionsAsync(city?.Name);
            if (response.IsSuccessful)
            {
                var items = response.Data.Select(promotion => new PromotionItemViewModel(promotion)).ToArray();
                Items.ReplaceWith(items);
            }
        }
    }
}