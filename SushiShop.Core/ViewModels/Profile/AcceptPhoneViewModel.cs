using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Managers.Profile;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Providers;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Profile
{
    public class AcceptPhoneViewModel : BasePageViewModel<RegistrationNavigationParameters>
    {
        private readonly IProfileManager profileManager;
        private readonly IUserDialogs userDialogs;
        private readonly IUserSession userSession;
        private string login;

        public AcceptPhoneViewModel(IProfileManager profileManager, IUserSession userSession)
        {
            this.profileManager = profileManager;
            this.userDialogs = UserDialogs.Instance;
            this.userSession = userSession;

            ContinueCommand = new SafeAsyncCommand(ExecutionStateWrapper, ContinueAsync);
        }

        private string? code;
        public string? Code
        {
            get => code;
            set => SetProperty(ref code, value);
        }

        public IMvxCommand ContinueCommand { get; }

        public override void Prepare(RegistrationNavigationParameters parameter)
        {
            login = parameter.Login;
        }

        private async Task ContinueAsync()
        {
            if (Code.IsNullOrEmpty())
            {
                return;
            }

            var response = await profileManager.AuthorizeAsync(login, Code);
            if (response.Data is null)
            {
                var error = response.Errors.FirstOrDefault();
                if (error.IsNullOrEmpty())
                {
                    return;
                }
                await userDialogs.AlertAsync(error);
                return;
            }

            userSession.SetToken(response.Data.Token);              
            await RefreshDataAsync();
            _ = NavigationManager.NavigateAsync<ProfileViewModel>();
        }
    }
}