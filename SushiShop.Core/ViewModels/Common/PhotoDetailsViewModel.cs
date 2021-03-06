using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;

namespace SushiShop.Core.ViewModels.Common
{
    public class PhotoDetailsViewModel : BasePageViewModel<string>
    {
        public string ImageUrl { get; private set; } = string.Empty;

        public override void Prepare(string parameter)
        {
            ImageUrl = parameter;

            RaisePropertyChanged(nameof(ImageUrl));
        }
    }
}