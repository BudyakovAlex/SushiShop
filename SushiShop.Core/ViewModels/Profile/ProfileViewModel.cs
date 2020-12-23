using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Managers.Profile;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Feedback;
using SushiShop.Core.ViewModels.Orders;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Profile
{
    public class ProfileViewModel : BasePageViewModel
    {
        private readonly IProfileManager profileManager;

        public ProfileViewModel(IProfileManager profileManager)
        {
            this.profileManager = profileManager;

            LogoutCommand = new SafeAsyncCommand(ExecutionStateWrapper, LogoutAsync);
            ShowPersonalDataViewCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowPersonalDataViewAsync);
            ShowMyOrdersViewCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowMyOrdersViewAsync);
            ShowFeedbackViewCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowFeedbackViewAsync);
            ShowScoreViewCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowScoreViewAsync);
            ChooseNewImageCommand = new SafeAsyncCommand(ExecutionStateWrapper, ChooseNewImageAsync);
        }

        private string login;
        public string Login
        {
            get => login;
            set => SetProperty(ref login, value);
        }

        private bool isLogged;
        public bool IsLogged
        {
            get => isLogged;
            set => SetProperty(ref isLogged, value);
        }

        private string avatar;
        public string Avatar
        {
            get => avatar;
            set => SetProperty(ref avatar, value);
        }

        private string username;
        public string Username
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

        public IMvxCommand ShowPersonalDataViewCommand { get; }

        public IMvxCommand ShowMyOrdersViewCommand { get; }

        public IMvxCommand ShowFeedbackViewCommand { get; }

        public IMvxCommand ShowScoreViewCommand { get; }

        public IMvxCommand ChooseNewImageCommand { get; }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            var response = await profileManager.GetDiscountAsync();
            if (response.Data is null)
            {
                var error = response.Errors.FirstOrDefault();
                if (error.IsNullOrEmpty())
                {
                    return;
                }
            }

            Score = response.Data.Bonuses;
        }

        private async Task LogoutAsync()
        {
            IsLogged = false;

            var confirmationTask = UserDialogs.Instance.ConfirmAsync(string.Empty, AppStrings.SignOutOfYourProfile, okText: AppStrings.No, cancelText: AppStrings.Yes);
            await Task.WhenAll(confirmationTask);

            //HACK: to avoid design issue
            var isConfirmed = confirmationTask.Result;
            if (isConfirmed)
            {
                return;
            }

            _ = Task.CompletedTask;
        }

        private Task ShowPersonalDataViewAsync()
        {
            return NavigationManager.NavigateAsync<EditProfileViewModel>();
        }

        private Task ShowMyOrdersViewAsync()
        {
            return NavigationManager.NavigateAsync<MyOrdersViewModel>();
        }

        private Task ShowFeedbackViewAsync()
        {
            return NavigationManager.NavigateAsync<FeedbackViewModel>();
        }

        private Task ShowScoreViewAsync()
        {
            return NavigationManager.NavigateAsync<BonusProgramViewModel>();
        }

        private Task ChooseNewImageAsync()
        {
            return Task.CompletedTask;
        }
    }
}