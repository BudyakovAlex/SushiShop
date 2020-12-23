using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Managers.Profile;
using SushiShop.Core.Providers;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Feedback;
using SushiShop.Core.ViewModels.Orders;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Profile
{
    public class ProfileViewModel : BasePageViewModel
    {
        private readonly IProfileManager profileManager;
        private readonly IUserSession userSession;
        private readonly IUserDialogs userDialogs;

        public ProfileViewModel(
            IProfileManager profileManager,
            IUserSession userSession,
            IUserDialogs userDialogs)
        {
            this.profileManager = profileManager;
            this.userSession = userSession;
            this.userDialogs = userDialogs;

            LogoutCommand = new SafeAsyncCommand(ExecutionStateWrapper, LogoutAsync);
            ShowEditProfileCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowEditProfileAsync);
            ShowMyOrdersCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowMyOrdersAsync);
            ShowFeedbackCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowFeedbackAsync);
            ShowBonusProgramCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowBonusProgramAsync);
            ChooseNewImageCommand = new SafeAsyncCommand(ExecutionStateWrapper, ChooseNewImageAsync);
            LoginCommand = new SafeAsyncCommand(ExecutionStateWrapper, LoginAsync, () => PhoneOrEmail.IsNotNullNorEmpty());
            RegistrationCommand = new SafeAsyncCommand(ExecutionStateWrapper, RegistrationAsync);
        }

        private string? phoneOrEmail;
        public string? PhoneOrEmail
        {
            get => phoneOrEmail;
            set => SetProperty(ref phoneOrEmail, value, LoginCommand.RaiseCanExecuteChanged);
        }

        private string? login;
        public string? Login
        {
            get => login;
            set => SetProperty(ref login, value);
        }

        private bool isAuthorized;
        public bool IsAuthorized
        {
            get => isAuthorized;
            set => SetProperty(ref isAuthorized, value);
        }

        private string? avatar;
        public string? Avatar
        {
            get => avatar;
            set => SetProperty(ref avatar, value);
        }

        private string? username;
        public string? Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        private int score;
        public int Score
        {
            get => score;
            set => SetProperty(ref score, value);
        }

        public IMvxCommand LogoutCommand { get; }

        public IMvxCommand ShowEditProfileCommand { get; }

        public IMvxCommand ShowMyOrdersCommand { get; }

        public IMvxCommand ShowFeedbackCommand { get; }

        public IMvxCommand ShowBonusProgramCommand { get; }

        public IMvxCommand ChooseNewImageCommand { get; }

        public IMvxCommand LoginCommand { get; }

        public IMvxCommand RegistrationCommand { get; }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            _ = SafeExecutionWrapper.WrapAsync(RefreshDataAsync);
        }

        protected override async Task RefreshDataAsync()
        {
            await base.RefreshDataAsync();

            var token = userSession.GetToken();
            var isUserAuthorized = token != null && token.ExpiresAt > DateTime.Now;
            IsAuthorized = isUserAuthorized;
            if (!IsAuthorized)
            {
                return;
            }

            var getDiscountTask = profileManager.GetDiscountAsync();
            var getProfileTask = profileManager.GetProfileAsync();

            await Task.WhenAll(getDiscountTask, getProfileTask);

            if (getProfileTask.Result.Data is null || getDiscountTask.Result.Data is null)
            {
                var error = getProfileTask.Result.Errors.FirstOrDefault();
                if (error.IsNullOrEmpty())
                {
                    return;
                }
            }

            var profile = getProfileTask.Result.Data!;
            Username = profile.FirstName;
            Login = profile.Email ?? profile.Phone;
            Avatar = profile.Photo?.JpgUrl;
            Score = getDiscountTask.Result.Data!.Bonuses;
        }

        private async Task LogoutAsync()
        {
            IsAuthorized = false;

            var confirmationTask = UserDialogs.Instance.ConfirmAsync(string.Empty, AppStrings.SignOutOfYourProfile, okText: AppStrings.No, cancelText: AppStrings.Yes);
            await Task.WhenAll(confirmationTask);

            //HACK: to avoid design issue
            var isConfirmed = confirmationTask.Result;
            if (isConfirmed)
            {
                return;
            }

            userSession.SetToken(null);
        }

        private async Task ShowEditProfileAsync()
        {
            var shouldRefresh = await NavigationManager.NavigateAsync<EditProfileViewModel>();
            if (!shouldRefresh)
            {
                return;
            }

            await RefreshDataAsync();
        }

        private Task ShowMyOrdersAsync()
        {
            return NavigationManager.NavigateAsync<MyOrdersViewModel>();
        }

        private Task ShowFeedbackAsync()
        {
            return NavigationManager.NavigateAsync<FeedbackViewModel>();
        }

        private Task ShowBonusProgramAsync()
        {
            return NavigationManager.NavigateAsync<BonusProgramViewModel>();
        }

        private Task ChooseNewImageAsync()
        {
            return Task.CompletedTask;
        }

        private async Task LoginAsync()
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

            var isConfirmed = await NavigationManager.NavigateAsync<ConfirmCodeViewModel, string>(PhoneOrEmail!);
            if (!isConfirmed)
            {
                return;
            }

            await RefreshDataAsync();
        }

        private async Task RegistrationAsync()
        {
            var isRegistered = await NavigationManager.NavigateAsync<RegistrationViewModel>();
            if (!isRegistered)
            {
                return;
            }

            await RefreshDataAsync();
        }
    }
}