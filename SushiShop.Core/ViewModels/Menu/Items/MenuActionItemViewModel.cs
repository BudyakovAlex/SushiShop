using System.Threading.Tasks;
using Acr.UserDialogs;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Common;

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

        private async Task ExecuteActionAsync()
        {

            switch (actionType)
            {
                case ActionType.Franchise:
                    {
                        await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig()
                        {
                            OkText = AppStrings.Yes,
                            CancelText = AppStrings.No,
                            Message = AppStrings.GoToTheFranchisePage_
                        });
                        return;
                    }
                case ActionType.Vacancies:
                    {
                         await NavigationManager.NavigateAsync<CommonInfoViewModel>();
                         return;
                    }
            }
            // return Task.CompletedTask;
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