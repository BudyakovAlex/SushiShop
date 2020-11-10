using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Managers.CommonInfo;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Resources;

namespace SushiShop.Core.ViewModels.Common
{
    public class CommonInfoViewModel : BasePageViewModel<CommonInfoNavigationParameters>
    {
        private readonly ICommonInfoManager commonInfoManager;

        private CommonInfoType commonInfoType;
        private string? city;

        public CommonInfoViewModel(ICommonInfoManager commonInfoManager)
        {
            this.commonInfoManager = commonInfoManager;
        }

        public string? Content { get; private set; }

        public string Title => GetTitle();

        public override void Prepare(CommonInfoNavigationParameters parameter)
        {
            commonInfoType = parameter.CommonInfoType;
            city = parameter.City;
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            //TODO: add all cases load here
            switch (commonInfoType)
            {
                case CommonInfoType.Vacancies:
                    var vacancies = await commonInfoManager.GetVacanciesAsync(city);
                    Content = vacancies.Data.Text;
                    break;
            }

            _ = RaisePropertyChanged(nameof(Content));
        }

        private string GetTitle()
        {
            return commonInfoType switch
            {
                CommonInfoType.AboutUs => AppStrings.AboutUs,
                CommonInfoType.PrivacyPolicy => AppStrings.PrivacyPolicy,
                CommonInfoType.PublicOffer => AppStrings.PublicOffer,
                CommonInfoType.Vacancies => AppStrings.Vacancies,
                _ => string.Empty
            };
        }
    }
}