using System;
using System.Linq;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.ViewModels;
using SushiShop.Core.Managers.Promotions;
using SushiShop.Core.Messages;
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
            Messenger.Subscribe<CityChangedMessage>(OnCityChnaged).DisposeWith(Disposables);
        }

        public MvxObservableCollection<PromotionItemViewModel> Items { get; }

        public string Title => AppStrings.Promotions;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            _ = ExecutionStateWrapper.WrapAsync(() => RefreshDataAsync(), awaitWhenBusy: true);
        }

        protected override async Task RefreshDataAsync()
        {
            await base.RefreshDataAsync();

            var city = userSession.GetCity();
            var response = await promotionsManager.GetPromotionsAsync(city?.Name);
            if (response.IsSuccessful)
            {
                var items = response.Data.Select(promotion => new PromotionItemViewModel(promotion)).ToArray();
                Items.ReplaceWith(items);
            }
        }

        private void OnCityChnaged(CityChangedMessage message)
        {
            RefreshDataCommand.Execute();
        }
    }
}