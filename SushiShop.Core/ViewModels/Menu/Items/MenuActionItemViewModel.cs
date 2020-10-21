using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.IoC;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Managers.CommonInfo;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Common;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class MenuActionItemViewModel : BaseViewModel
    {
        private readonly ICommonInfoManager commonInfoManager;
        private readonly ActionType actionType;

        public MenuActionItemViewModel(ActionType actionType)
        {
            this.actionType = actionType;
            commonInfoManager = BaseCompositionRoot.Container.Resolve<ICommonInfoManager>();

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
            var getFranchiseTask = commonInfoManager.GetFranchiseAsync();
            var confirmationTask = UserDialogs.Instance.ConfirmAsync(AppStrings.GoToTheFranchisePage, okText: AppStrings.Yes, cancelText: AppStrings.No);
            await Task.WhenAll(getFranchiseTask, confirmationTask);

            var isConfirmed = confirmationTask.Result;
            if (!isConfirmed)
            {
                return;
            }

            await Browser.OpenAsync(getFranchiseTask.Result.Data.Url, BrowserLaunchMode.SystemPreferred);
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