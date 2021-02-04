using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Managers.Profile;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Plugins;
using SushiShop.Core.Providers;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Profile
{
    public class RegistrationViewModel : BasePageViewModelResult<bool>
    {
        private readonly IProfileManager profileManager;
        private readonly IUserSession userSession;
        private readonly IDialog dialog;

        public RegistrationViewModel(
            IProfileManager profileManager,
            IUserSession userSession,
            IDialog dialog)
        {
            this.profileManager = profileManager;
            this.userSession = userSession;
            this.dialog = dialog;

            RegisterCommand = new SafeAsyncCommand(ExecutionStateWrapper, RegisterAsync);
            ShowPrivacyPolicyCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowPrivacyPolicyAsync);
        }

        private string? fullName;
        public string? FullName
        {
            get => fullName;
            set => SetProperty(ref fullName, value);
        }

        private DateTime? dateOfBirth;
        public DateTime? DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                if (value == null)
                {
                    return;
                }

                SetProperty(ref dateOfBirth, value);
            }
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
            if (FullName.IsNullOrEmpty() ||
                DateOfBirth.Equals(default) ||
                Phone.IsNullOrEmpty() ||
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
                GenderType.Unknown,
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

                await dialog.ShowToastAsync(error);
                return;
            }

            var navigationParameters = new ConfirmCodeNavigationParameters(
                Phone!,
                response.Data.Message,
                response.Data.Placeholder);
            var isConfirmed = await NavigationManager.NavigateAsync<ConfirmCodeViewModel, ConfirmCodeNavigationParameters, bool>(navigationParameters);
            if (!isConfirmed)
            {
                return;
            }

            await NavigationManager.CloseAsync(this, true);
        }

        private Task ShowPrivacyPolicyAsync()
        {
            var city = userSession.GetCity();
            var navigationParameters = new CommonInfoNavigationParameters(CommonInfoType.Content, 0, city?.Name, Constants.Rest.PrivacyPolicyResource, AppStrings.PrivacyPolicy);
            return NavigationManager.NavigateAsync<CommonInfoViewModel, CommonInfoNavigationParameters>(navigationParameters);
        }
    }
}