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
    public class ConfirmCodeViewModel : BasePageViewModel<string>
    {
        private readonly IProfileManager profileManager;
        private readonly IUserDialogs userDialogs;

        private string login;

        public ConfirmCodeViewModel(IProfileManager profileManager, IUserDialogs userDialogs)
        {
            login = string.Empty;

            this.profileManager = profileManager;
            this.userDialogs = userDialogs;

            ContinueCommand = new SafeAsyncCommand(ExecutionStateWrapper, ContinueAsync, () => Code.IsNotNullNorEmpty());
        }

        private string? code;
        public string? Code
        {
            get => code;
            set => SetProperty(ref code, value, ContinueCommand.RaiseCanExecuteChanged);
        }

        public IMvxCommand ContinueCommand { get; }

        public override void Prepare(string parameter)
        {
            login = parameter;
        }

        private async Task ContinueAsync()
        {
            var response = await profileManager.AuthorizeAsync(login, Code!);
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
            
            await NavigationManager.NavigateAsync<ProfileViewModel>();
        }
    }
}