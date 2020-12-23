using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Managers.Profile;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Profile
{
    public class AuthorizationViewModel : BasePageViewModel
    {
        private readonly IProfileManager profileManager;
        private readonly IUserDialogs userDialogs;

        public AuthorizationViewModel(IProfileManager profileManager, IUserDialogs userDialogs)
        {
            this.profileManager = profileManager;
            this.userDialogs = UserDialogs.Instance;

            SignInCommand = new SafeAsyncCommand(ExecutionStateWrapper, SignInAsync);
            SignUpCommand = new SafeAsyncCommand(ExecutionStateWrapper, SignUpAsync);
        }

        private string? phoneOrEmail;
        public string? PhoneOrEmail
        {
            get => phoneOrEmail;
            set => SetProperty(ref phoneOrEmail, value);
        }

        private bool isExistLogin;
        public bool IsExistLogin
        {
            get => isExistLogin;
            set => SetProperty(ref isExistLogin, value);
        }

        public IMvxCommand SignInCommand { get; }
        public IMvxCommand SignUpCommand { get; }

        private async Task SignInAsync()
        {
            var response = await profileManager.CheckIsLoginAvailableAsync(PhoneOrEmail, null);
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

            await NavigationManager.NavigateAsync<ConfirmCodeViewModel>();
        }

        private Task SignUpAsync()
        {
            return NavigationManager.NavigateAsync<RegistrationViewModel>();
        }
    }
}