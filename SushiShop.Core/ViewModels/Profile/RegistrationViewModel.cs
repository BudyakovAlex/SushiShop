using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;

namespace SushiShop.Core.ViewModels.Profile
{
    public class RegistrationViewModel : BasePageViewModel
    {
        public RegistrationViewModel()
        {
            RegisterCommand = new SafeAsyncCommand(ExecutionStateWrapper, RegisterAsync);
            ShowPrivacyPolicyCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowPrivacyPolicyAsync);
        }

        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private string dateOfBirth;
        public string DateOfBirth
        {
            get => dateOfBirth;
            set => SetProperty(ref name, value);
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

        private bool acceptEmalhNotifications;
        public bool AcceptEmailNotifications
        {
            get => acceptEmalhNotifications;
            set => SetProperty(ref acceptEmalhNotifications, value);
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
