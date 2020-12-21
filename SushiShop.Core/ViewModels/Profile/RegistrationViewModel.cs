using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Managers.Profile;

namespace SushiShop.Core.ViewModels.Profile
{
    public class RegistrationViewModel : BasePageViewModel
    {
        private readonly IProfileManager profileManager;

        public RegistrationViewModel(IProfileManager profileManager)
        {
            this.profileManager = profileManager;

            RegisterCommand = new SafeAsyncCommand(ExecutionStateWrapper, RegisterAsync);
            ShowPrivacyPolicyCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowPrivacyPolicyAsync);
        }

        private string fullName;
        public string FullName
        {
            get => fullName;
            set => SetProperty(ref fullName, value);
        }

        private string dateOfBirth;
        public string DateOfBirth
        {
            get => dateOfBirth;
            set => SetProperty(ref dateOfBirth, value);
        }

        private string phone;
        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }

        private string email;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        private bool acceptPushNotifications;
        public bool AcceptPushNotifications
        {
            get => acceptPushNotifications;
            set => SetProperty(ref acceptPushNotifications, value);
        }

        private bool acceptEmailNotifications;
        public bool AcceptEmailNotifications
        {
            get => acceptEmailNotifications;
            set => SetProperty(ref acceptEmailNotifications, value);
        }

        private bool acceptSmshNotifications;
        public bool AcceptSmsNotifications
        {
            get => acceptSmshNotifications;
            set => SetProperty(ref acceptSmshNotifications, value);
        }

        public IMvxCommand RegisterCommand { get; }

        public IMvxCommand ShowPrivacyPolicyCommand { get; }

        private Task RegisterAsync()
        {
            return NavigationManager.NavigateAsync<AcceptPhoneViewModel>();
        }

        private Task ShowPrivacyPolicyAsync()
        {
            return Task.CompletedTask;
        }
    }
}
