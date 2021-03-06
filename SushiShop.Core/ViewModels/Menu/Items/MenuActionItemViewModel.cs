using System.Threading.Tasks;
using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.IoC;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Managers.CommonInfo;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Common;
using Xamarin.Essentials;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class MenuActionItemViewModel : BaseViewModel
    {
        private readonly ICommonInfoManager commonInfoManager;
        private readonly ActionType actionType;
        private readonly City? city;

        public MenuActionItemViewModel(ActionType actionType, City? city)
        {
            this.actionType = actionType;
            this.city = city;
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
                ActionType.Vacancies => ShowVacanciesAsync(),
                _ => Task.CompletedTask
            };
        }

        private Task ShowVacanciesAsync()
        {
            var commonInfoNavigationParams = new CommonInfoNavigationParameters(CommonInfoType.Vacancies, city?.Name);
            return NavigationManager.NavigateAsync<CommonInfoViewModel, CommonInfoNavigationParameters>(commonInfoNavigationParams);
        }

        private async Task ShowFranchisePopupAsync()
        {
            var getFranchiseTask = commonInfoManager.GetFranchiseAsync();
            var confirmationTask = UserDialogs.Instance.ConfirmAsync(string.Empty, AppStrings.GoToTheFranchisePage, okText: AppStrings.No, cancelText: AppStrings.Yes);
            await Task.WhenAll(getFranchiseTask, confirmationTask);

            //HACK: to avoid design issue
            var isConfirmed = confirmationTask.Result;
            if (isConfirmed)
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