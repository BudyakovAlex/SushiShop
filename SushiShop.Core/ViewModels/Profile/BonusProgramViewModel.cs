using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.ViewModels;
using SushiShop.Core.Managers.CommonInfo;
using SushiShop.Core.ViewModels.Profile.Items;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Profile
{
    public class BonusProgramViewModel : BasePageViewModel
    {
        private readonly ICommonInfoManager commonInfoManager;
        private readonly IUserDialogs userDialogs;

        public BonusProgramViewModel(ICommonInfoManager commonInfoManager)
        {
            this.commonInfoManager = commonInfoManager;
            this.userDialogs = UserDialogs.Instance;

            Images = new MvxObservableCollection<BonusProgramImageItemViewModel>();
        }

        private string? title;
        public string? Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private string? introText;
        public string? IntroText
        {
            get => introText;
            set => SetProperty(ref introText, value);
        }

        private string? content;
        public string? Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }

        public MvxObservableCollection<BonusProgramImageItemViewModel> Images { get; }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var getBonusesContentTask = commonInfoManager.GetBonusesContentAsync();
            var getBonusesImagesTask = commonInfoManager.GetBonusesImagesAsync();

            await Task.WhenAll(getBonusesContentTask, getBonusesImagesTask);

            var bonusesImagesResponse = getBonusesImagesTask.Result;
            var bonusesResponse = getBonusesContentTask.Result;
            if (bonusesResponse.Data is null)
            {
                var error = bonusesResponse.Errors.FirstOrDefault();
                if (error.IsNullOrEmpty())
                {
                    return;
                }

                await userDialogs.AlertAsync(error);
                return;
            }

            if (bonusesImagesResponse.Data is null)
            {
                var error = bonusesImagesResponse.Errors.FirstOrDefault();
                if (error.IsNullOrEmpty())
                {
                    return;
                }

                await userDialogs.AlertAsync(error);
                return;
            }

            Title = bonusesResponse.Data?.Title;
            Content = bonusesResponse.Data?.MainText;

            var imagesViewModels = bonusesImagesResponse.Data.Select(image => new BonusProgramImageItemViewModel(image)).ToArray();
            Images.ReplaceWith(imagesViewModels);
        }
    }
}