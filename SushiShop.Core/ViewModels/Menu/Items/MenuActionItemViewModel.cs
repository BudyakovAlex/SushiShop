using Acr.UserDialogs;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Common;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class MenuActionItemViewModel : BaseViewModel
    {
        private readonly ActionType actionType;

        public MenuActionItemViewModel(ActionType actionType)
        {
            this.actionType = actionType;

            ActionCommand = new SafeAsyncCommand(ExecutionStateWrapper, ExecuteActionAsync);
        }

        public string Title => GetTitle();

        public IMvxCommand ActionCommand { get; }

        private Task ExecuteActionAsync()
        {
            return actionType switch
            {
                ActionType.Franchise => ShowFranchisePopupAsync(),
                ActionType.Vacancies => NavigationManager.NavigateAsync<CommonInfoViewModel>(),
                _ => Task.CompletedTask
            };
        }

        private async Task ShowFranchisePopupAsync()
        {
            var isConfirmed = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig()
            {
                OkText = AppStrings.Yes,
                CancelText = AppStrings.No,
                Message = AppStrings.GoToTheFranchisePage_
            });

            if (isConfirmed)
                return;
            await Browser.OpenAsync("http://www.xamarin.com", BrowserLaunchMode.SystemPreferred);
        }

        private string GetTitle()
        {
            return actionType switch
            {
                ActionType.Franchise => AppStrings.Franchise,
                ActionType.Vacancies => AppStrings.Vacancies,
                _ => string.Empty
            };
        }
    }
}