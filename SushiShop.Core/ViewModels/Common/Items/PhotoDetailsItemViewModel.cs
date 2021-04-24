using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;

namespace SushiShop.Core.ViewModels.Common.Items
{
    public class PhotoDetailsItemViewModel : BaseViewModel
    {
        public PhotoDetailsItemViewModel(string imagePath)
        {
            ImagePath = imagePath;
        }

        public string ImagePath { get; }
    }
}
