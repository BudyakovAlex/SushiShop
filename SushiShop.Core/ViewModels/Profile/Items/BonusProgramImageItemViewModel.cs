using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.ViewModels.Profile.Items
{
    public class BonusProgramImageItemViewModel : BaseViewModel
    {
        public BonusProgramImageItemViewModel(LinkedImage linkedImage)
        {
            ImageUrl = linkedImage.ImageUrl;
        }

        public string? ImageUrl { get; }
    }
}