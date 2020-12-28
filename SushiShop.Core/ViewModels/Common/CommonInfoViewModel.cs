using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Managers.CommonInfo;
using SushiShop.Core.NavigationParameters;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Common
{
    public class CommonInfoViewModel : BasePageViewModel<CommonInfoNavigationParameters>
    {
        private readonly ICommonInfoManager commonInfoManager;

        private CommonInfoNavigationParameters? parameter;

        public CommonInfoViewModel(ICommonInfoManager commonInfoManager)
        {
            this.commonInfoManager = commonInfoManager;
        }

        public string? Content { get; private set; }

        public string? Title { get; private set; }

        public override void Prepare(CommonInfoNavigationParameters parameter)
        {
            this.parameter = parameter;

            Title = parameter.Title;
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            if (parameter is null)
            {
                return;
            }

            switch (parameter.CommonInfoType)
            {
                case CommonInfoType.Vacancies:
                    var vacanciesResponse = await commonInfoManager.GetVacanciesAsync(parameter.City);
                    Content = vacanciesResponse.Data?.Text;
                    break;
                case CommonInfoType.Content:
                    var contentResponse = await commonInfoManager.GetContentAsync(parameter.Alias!, parameter.Id!.Value, parameter.City);
                    Content = contentResponse.Data?.MainText;
                    break;
            }

            _ = RaisePropertyChanged(nameof(Content));
        }
    }
}