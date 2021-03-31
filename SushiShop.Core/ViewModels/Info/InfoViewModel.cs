using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Common;
using SushiShop.Core.Managers.CommonInfo;
using SushiShop.Core.Messages;
using SushiShop.Core.Providers;
using SushiShop.Core.ViewModels.Info.Items;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace SushiShop.Core.ViewModels.Info
{
    public class InfoViewModel : BaseItemsPageViewModel<InfoMenuItemViewModel>
    {
        private readonly ICommonInfoManager commonInfoManager;
        private readonly IUserSession userSession;

        public InfoViewModel(ICommonInfoManager commonInfoManager, IUserSession userSession)
        {
            this.commonInfoManager = commonInfoManager;
            this.userSession = userSession;

            SocialNetworks = new MvxObservableCollection<SocialNetworkItemViewModel>();

            GoToShopsCommand = new SafeAsyncCommand(ExecutionStateWrapper, GoToShopsAsync);
            CallToOfficeCommand = new MvxCommand(() => ExecutionStateWrapper.Wrap(CallToOffice));
            OpenSiteInBrowserCommand = new SafeAsyncCommand(ExecutionStateWrapper, OpenSiteInBrowserAsync);

            Messenger.Subscribe<CityChangedMessage>(OnCityChnaged).DisposeWith(Disposables);
        }

        public ICommand CallToOfficeCommand { get; }

        public ICommand GoToShopsCommand { get; }

        public ICommand OpenSiteInBrowserCommand { get; }

        public string? OfficePhone { get; private set; }

        public bool HasOfficePhone => OfficePhone.IsNotNullNorEmpty();

        public MvxObservableCollection<SocialNetworkItemViewModel> SocialNetworks { get; }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

           _ = SafeExecutionWrapper.Wrap(RefreshDataAsync);
        }

        protected override async Task RefreshDataAsync()
        {
            await base.RefreshDataAsync();

            var city = userSession.GetCity();
            OfficePhone = city?.Phone;

            var getMenuListTask = commonInfoManager.GetCommonInfoMenuAsync();
            var getSocialNetworksTask = commonInfoManager.GetSocialNetworksAsync();

            await Task.WhenAll(
                getMenuListTask,
                getSocialNetworksTask,
                RaisePropertyChanged(nameof(OfficePhone)),
                RaisePropertyChanged(nameof(HasOfficePhone)));

            if (!getMenuListTask.Result.IsSuccessful)
            {
                return;
            }

            var menuItemViewModels = getMenuListTask.Result.Data.Select(item => new InfoMenuItemViewModel(userSession, item)).ToArray();
            Items.ReplaceWith(menuItemViewModels);

            if (!getSocialNetworksTask.Result.IsSuccessful)
            {
                return;
            }

            var socialNetworkViewModels = getSocialNetworksTask.Result.Data.Select(item => new SocialNetworkItemViewModel(item)).ToArray();
            SocialNetworks.ReplaceWith(socialNetworkViewModels);
        }

        private void CallToOffice()
        {
            SafeExecutionWrapper.Wrap(() => PhoneDialer.Open(OfficePhone));  
        }

        private Task GoToShopsAsync()
        {
            return NavigationManager.NavigateAsync<ShopsViewModel>();
        }

        private void OnCityChnaged(CityChangedMessage message)
        {
            RefreshDataCommand.Execute();
        }

        private Task OpenSiteInBrowserAsync()
        {
            return Browser.OpenAsync(Constants.Info.DevelopersSite);
        }
    }
}