using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Models.Promotions;
using SushiShop.Core.ViewModels.Promotions;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class MenuPromotionItemViewModel : BaseViewModel
    {
        private readonly Promotion promotion;

        public MenuPromotionItemViewModel(Promotion promotion)
        {
            this.promotion = promotion;

            ShowDetailsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowDetailsAsync);
        }

        public IMvxAsyncCommand ShowDetailsCommand { get; }

        public string? ImageUrl => promotion.RectangularImageInfo.JpgUrl;

        private Task ShowDetailsAsync() =>
            NavigationManager.NavigateAsync<PromotionDetailsViewModel, long>(promotion.Id);
    }
}
