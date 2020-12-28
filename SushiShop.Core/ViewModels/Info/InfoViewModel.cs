using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using MvvmCross.Commands;
using SushiShop.Core.Common;
using SushiShop.Core.Managers.CommonInfo;
using SushiShop.Core.Providers;
using SushiShop.Core.ViewModels.Info.Items;
using System;
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

            GoToShopsCommand = new SafeAsyncCommand(ExecutionStateWrapper, GoToShopsAsync);
            CallToOfficeCommand = new MvxCommand(() => ExecutionStateWrapper.Wrap(CallToOffice));
        }

        private void CallToOffice()
        {
            PhoneDialer.Open(Constants.Info.OfficePhone);
        }

        private Task GoToShopsAsync()
        {
            throw new NotImplementedException();
        }

        public override Task InitializeAsync()
        {
            return Task.WhenAll(base.InitializeAsync(), RefreshDataAsync());
        }

        public ICommand CallToOfficeCommand { get; }

        public ICommand GoToShopsCommand { get; }

        protected override async Task RefreshDataAsync()
        {
            await base.RefreshDataAsync();
            var response = await commonInfoManager.GetCommonInfoMenuAsync();
            if (!response.IsSuccessful)
            {
                return;
            }

            var viewModels = response.Data.Select(item => new InfoMenuItemViewModel(userSession, item)).ToArray();
            Items.ReplaceWith(viewModels);
        }
    }
}