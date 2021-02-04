using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Models.Common;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace SushiShop.Core.ViewModels.Info.Items
{
    public class SocialNetworkItemViewModel : BaseViewModel
    {
        private readonly LinkedImage socialNetwork;

        public SocialNetworkItemViewModel(LinkedImage socialNetwork)
        {
            this.socialNetwork = socialNetwork;

            ImageUrl = socialNetwork.ImageUrl;

            OpenBrowserCommand = new SafeAsyncCommand(ExecutionStateWrapper, OpenBrowserAsync);
        }

        public ICommand OpenBrowserCommand { get; }

        public string ImageUrl { get; }

        private Task OpenBrowserAsync()
        {
            return Browser.OpenAsync(socialNetwork.Url, BrowserLaunchMode.External);
        }
    }
}