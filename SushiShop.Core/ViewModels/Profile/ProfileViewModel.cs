using System;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.Common.Wrappers;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using Plugin.Media.Abstractions;
using SushiShop.Core.Data.Models.Plugins;
using SushiShop.Core.Extensions;
using SushiShop.Core.Managers.Profile;
using SushiShop.Core.Plugins;
using SushiShop.Core.Providers;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Feedback;
using SushiShop.Core.ViewModels.Orders;

namespace SushiShop.Core.ViewModels.Profile
{
    public class ProfileViewModel : BasePageViewModel
    {
        private readonly IProfileManager profileManager;
        private readonly IUserSession userSession;
        private readonly IUserDialogs userDialogs;
        private readonly IDialog dialog;
        private readonly IMedia media;

        public ProfileViewModel(
            IProfileManager profileManager,
            IUserSession userSession,
            IUserDialogs userDialogs,
            IDialog dialog,
            IMedia media)
        {
            this.profileManager = profileManager;
            this.userSession = userSession;
            this.userDialogs = userDialogs;
            this.dialog = dialog;
            this.media = media;

            LogoutCommand = new SafeAsyncCommand(ExecutionStateWrapper, LogoutAsync);
            ShowEditProfileCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowEditProfileAsync);
            ShowMyOrdersCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowMyOrdersAsync);
            ShowFeedbackCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowFeedbackAsync);
            ShowBonusProgramCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowBonusProgramAsync);
            ChooseNewImageCommand = new SafeAsyncCommand(new ExecutionStateWrapper(), ChooseNewImageAsync);
            LoginCommand = new SafeAsyncCommand(ExecutionStateWrapper, LoginAsync, PhoneOrEmail.IsNotNullNorEmpty);
            RegistrationCommand = new SafeAsyncCommand(ExecutionStateWrapper, RegistrationAsync);

            PickPhotoCommand = new SafeAsyncCommand(ExecutionStateWrapper, PickPhotoAsync);
            TakePhotoCommand = new SafeAsyncCommand(ExecutionStateWrapper, TakePhotoAsync);
        }

        public IMvxCommand LogoutCommand { get; }
        public IMvxCommand ShowEditProfileCommand { get; }
        public IMvxCommand ShowMyOrdersCommand { get; }
        public IMvxCommand ShowFeedbackCommand { get; }
        public IMvxCommand ShowBonusProgramCommand { get; }
        public IMvxCommand ChooseNewImageCommand { get; }
        public IMvxCommand LoginCommand { get; }
        public IMvxCommand RegistrationCommand { get; }

        private string? phoneOrEmail;
        public string? PhoneOrEmail
        {
            get => phoneOrEmail;
            set => SetProperty(ref phoneOrEmail, value, LoginCommand.RaiseCanExecuteChanged);
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

        private IMvxAsyncCommand PickPhotoCommand { get; }
        private IMvxAsyncCommand TakePhotoCommand { get; }

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
            Username = profile.FullName;
            Avatar = profile.Photo?.JpgUrl;
            Score = getDiscountTask.Result.Data!.Bonuses;
        }

        private async Task LogoutAsync()
        {
            //HACK: to avoid design issue
            var isConfirmed = await UserDialogs.Instance.ConfirmAsync(
                string.Empty,
                AppStrings.SignOutOfYourProfile,
                okText: AppStrings.No,
                cancelText: AppStrings.Yes);
            if (isConfirmed)
            {
                return;
            }

            userSession.SetToken(null);
            IsAuthorized = false;
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
            return dialog.ShowActionSheetAsync(
                null,
                null,
                AppStrings.Cancel,
                new DialogAction(AppStrings.TakePhoto, TakePhotoCommand),
                new DialogAction(AppStrings.UploadFromGallery, PickPhotoCommand));
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

            var isConfirmed = await NavigationManager.NavigateAsync<ConfirmCodeViewModel, string, bool>(PhoneOrEmail!);
            if (!isConfirmed)
            {
                return;
            }

            IsAuthorized = true;

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

        private async Task PickPhotoAsync()
        {
            var mediaFile = await media.PickPhotoOrDefaultAsync();
            if (mediaFile is null)
            {
                return;
            }

            await UploadPhotoAsync(mediaFile.Path);
        }

        private async Task TakePhotoAsync()
        {
            var mediaFile = await media.TakePhotoOrDefaultAsync();
            if (mediaFile is null)
            {
                return;
            }

            await UploadPhotoAsync(mediaFile.Path);
        }

        private async Task UploadPhotoAsync(string imagePath)
        {
            var response = await profileManager.UploadPhotoAsync(imagePath);
            if (response.IsSuccessful)
            {
                Avatar = response.Data;
            }
        }
    }
}