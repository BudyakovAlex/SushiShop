using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Managers.Profile;
using SushiShop.Core.NavigationParameters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Profile
{
    public class RegistrationViewModel : BasePageViewModel
    {
        private readonly IProfileManager profileManager;
        private readonly IUserDialogs userDialogs;

        public RegistrationViewModel(IProfileManager profileManager)
        {
            this.profileManager = profileManager;
            this.userDialogs = UserDialogs.Instance;

            RegisterCommand = new SafeAsyncCommand(ExecutionStateWrapper, RegisterAsync);
            ShowPrivacyPolicyCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowPrivacyPolicyAsync);
        }

        private string? fullName;
        public string? FullName
        {
            get => fullName;
            set => SetProperty(ref fullName, value);
        }

        private GenderType gender = GenderType.None;
        public GenderType Gender
        {
            get => gender;
            set => SetProperty(ref gender, value);
        }

        private DateTime dateOfBirth;
        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set => SetProperty(ref dateOfBirth, value);
        }

        private string? phone;
        public string? Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }

        private string? email;
        public string? Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        private bool isAcceptPushNotifications;
        public bool IsAcceptPushNotifications
        {
            get => isAcceptPushNotifications;
            set => SetProperty(ref isAcceptPushNotifications, value);
        }

        private bool isAcceptEmailNotifications;
        public bool IsAcceptEmailNotifications
        {
            get => isAcceptEmailNotifications;
            set => SetProperty(ref isAcceptEmailNotifications, value);
        }

        private bool isAcceptSmshNotifications;
        public bool IsAcceptSmsNotifications
        {
            get => isAcceptSmshNotifications;
            set => SetProperty(ref isAcceptSmshNotifications, value);
        }

        public IMvxCommand RegisterCommand { get; }

        public IMvxCommand ShowPrivacyPolicyCommand { get; }

        private async Task RegisterAsync()
        {
            if (FullName.IsNullOrEmpty() &&
                DateOfBirth.Equals(default) &&
                Phone.IsNullOrEmpty() &&
                Email.IsNullOrEmpty())
            {
                return;
            }

            var profile = new Data.Models.Profile.Profile(
                Email,
                Phone,
                DateOfBirth,
                string.Empty,
                string.Empty,
                FullName,
                Gender,
                IsAcceptEmailNotifications,
                IsAcceptSmsNotifications,
                IsAcceptPushNotifications,
                true);

            var response = await profileManager.RegistrationAsync(profile);
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

            await RefreshDataAsync();

            var navigationParameters = new RegistrationNavigationParameters(response.Data.Phone);
            await NavigationManager.NavigateAsync<ConfirmCodeViewModel, RegistrationNavigationParameters>(navigationParameters);
        }

        private Task ShowPrivacyPolicyAsync()
        {
            return Task.CompletedTask;
        }
    }
}