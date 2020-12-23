using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.ViewModels.Feedback;
using SushiShop.Core.ViewModels.Orders;

namespace SushiShop.Core.ViewModels.Profile
{
    public class ProfileViewModel : BasePageViewModel
    {
        public ProfileViewModel()
        {
            LoginCommand = new SafeAsyncCommand(ExecutionStateWrapper, LoginAsync);
            RegistrationCommand = new SafeAsyncCommand(ExecutionStateWrapper, RegistrationAsync);
            LogoutCommand = new SafeAsyncCommand(ExecutionStateWrapper, LogoutAsync);
            ShowPersonalDataViewCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowPersonalDataViewAsync);
            ShowMyOrdersViewCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowMyOrdersViewAsync);
            ShowFeedbackViewCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowFeedbackViewAsync);
            ShowScoreViewCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowScoreViewAsync);
            ChooseNewImageCommand = new SafeAsyncCommand(ExecutionStateWrapper, ChooseNewImageAsync);
        }

        private string? login;
        public string? Login
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

        // TODO: Change this code.
        public string UserName => "Александр";
        public string Score => 2020 + " баллов";

        public IMvxCommand LogoutCommand { get; }

        public IMvxCommand LoginCommand { get; }

        public IMvxCommand RegistrationCommand { get; }

        public IMvxCommand ShowPersonalDataViewCommand { get; }

        public IMvxCommand ShowMyOrdersViewCommand { get; }

        public IMvxCommand ShowFeedbackViewCommand { get; }

        public IMvxCommand ShowScoreViewCommand { get; }

        public IMvxCommand ChooseNewImageCommand { get; }

        private Task LoginAsync()
        {
            IsLogged = true;
            return NavigationManager.NavigateAsync<AcceptPhoneViewModel>();
        }

        private Task RegistrationAsync()
        {
            return NavigationManager.NavigateAsync<RegistrationViewModel>();
        }

        private Task LogoutAsync()
        {
            IsLogged = false;
            return Task.CompletedTask;
        }

        private Task ShowPersonalDataViewAsync()
        {
            return NavigationManager.NavigateAsync<PersonalDataViewModel>();
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