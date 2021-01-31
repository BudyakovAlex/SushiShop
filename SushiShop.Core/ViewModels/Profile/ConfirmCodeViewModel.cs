using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Managers.Profile;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Plugins;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Profile
{
    public class ConfirmCodeViewModel : BasePageViewModel<ConfirmCodeNavigationParameters, bool>
    {
        private readonly IProfileManager profileManager;
        private readonly IDialog dialog;

        private string login;

        public ConfirmCodeViewModel(IProfileManager profileManager, IDialog dialog)
        {
            login = string.Empty;

            this.profileManager = profileManager;
            this.dialog = dialog;

            ContinueCommand = new SafeAsyncCommand(ExecutionStateWrapper, ContinueAsync, () => Code.IsNotNullNorEmpty());
        }

        public string? Message { get; private set; }

        public string? Placeholder { get; private set; }

        private string? code;
        public string? Code
        {
            get => code;
            set => SetProperty(ref code, value, ContinueCommand.RaiseCanExecuteChanged);
        }

        public IMvxCommand ContinueCommand { get; }

        public override void Prepare(ConfirmCodeNavigationParameters parameter)
        {
            login = parameter.Login;
            Message = parameter.Message;
            Placeholder = parameter.Placeholder;

            RaisePropertyChanged(nameof(Message));
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

                await dialog.ShowToastAsync(error);
                return;
            }

            await NavigationManager.CloseAsync(this, true);
        }
    }
}