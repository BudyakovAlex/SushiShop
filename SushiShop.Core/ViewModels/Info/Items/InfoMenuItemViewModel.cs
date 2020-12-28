using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Providers;
using SushiShop.Core.ViewModels.Common;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace SushiShop.Core.ViewModels.Info.Items
{
    public class InfoMenuItemViewModel : BaseViewModel
    {
        private readonly IUserSession userSession;

        private readonly CommonMenu commonMenu;

        public InfoMenuItemViewModel(IUserSession userSession, CommonMenu commonMenu)
        {
            this.userSession = userSession;
            this.commonMenu = commonMenu;

            ExecuteMenuActionCommand = new SafeAsyncCommand(ExecutionStateWrapper, ExecuteMenuActionAsync);
        }

        public string? Name => commonMenu.Title;

        public ICommand ExecuteMenuActionCommand { get; }

        private async Task ExecuteMenuActionAsync()
        {
            if (commonMenu.ExtraData is null)
            {
                var city = userSession.GetCity();
                var navigationParameters = new CommonInfoNavigationParameters(CommonInfoType.Content, commonMenu.Id, city?.Name, commonMenu.Alias, commonMenu.Title);
                await NavigationManager.NavigateAsync<CommonInfoViewModel, CommonInfoNavigationParameters>(navigationParameters);
                return;
            }

            switch (commonMenu.ExtraData.Type)
            {
                case ExtraDataType.Email:
                    await Email.ComposeAsync(string.Empty, string.Empty, commonMenu.ExtraData.Data);
                    break;
                case ExtraDataType.Phone:
                    PhoneDialer.Open(commonMenu.ExtraData.Data);
                    break;
                case ExtraDataType.Url:
                    await Browser.OpenAsync(commonMenu.ExtraData.Data);
                    break;
            }
        }
    }
}