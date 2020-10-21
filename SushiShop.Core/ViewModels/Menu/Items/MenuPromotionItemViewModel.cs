using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Models.Promotions;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class MenuPromotionItemViewModel : BaseViewModel
    {
        public MenuPromotionItemViewModel(Promotion promotion)
        {
            ImageUrl = promotion.RectangularImageInfo.JpgUrl;
        }

        public string ImageUrl { get; }
    }
}
