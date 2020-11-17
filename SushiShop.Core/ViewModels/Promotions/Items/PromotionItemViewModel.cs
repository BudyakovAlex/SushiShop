using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Models.Promotions;

namespace SushiShop.Core.ViewModels.Promotions.Items
{
    public class PromotionItemViewModel : BaseViewModel
    {
        private readonly Promotion promotion;

        public PromotionItemViewModel(Promotion promotion)
        {
            this.promotion = promotion;

            ShowDetailsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowDetailsAsync);
        }

        public IMvxAsyncCommand ShowDetailsCommand { get; }

        public string ImageUrl => promotion.RectangularImageInfo.JpgUrl;

        private Task ShowDetailsAsync() =>
            NavigationManager.NavigateAsync<PromotionDetailsViewModel, int>(promotion.Id);
    }
}
