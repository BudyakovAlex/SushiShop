using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Managers.CommonInfo;
using SushiShop.Core.Providers;
using System.Linq;
using System.Threading.Tasks;
using static SushiShop.Core.Common.Constants;

namespace SushiShop.Core.ViewModels.Profile
{
    public class BonusProgramViewModel : BasePageViewModel
    {
        private readonly ICommonInfoManager commonInfoManager;
        private readonly IUserDialogs userDialogs;
        private readonly IUserSession userSession;

        public BonusProgramViewModel(ICommonInfoManager commonInfoManager, IUserSession userSession)
        {
            this.commonInfoManager = commonInfoManager;
            this.userSession = userSession;
            this.userDialogs = UserDialogs.Instance;
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var city = userSession.GetCity()?.Name;
            var response = await commonInfoManager.GetContentAsync(Rest.BonusPolicyAlias, int.MinValue, city);

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

            Title = response.Data?.Title;
            IntroText = response.Data?.IntroText;
            Content = response.Data?.MainText;
        }

        private string title;

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private string introText;

        public string IntroText
        {
            get => introText;
            set => SetProperty(ref introText, value);
        }

        private string content;

        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }
    }
}