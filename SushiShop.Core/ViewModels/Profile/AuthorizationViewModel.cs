using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Managers.Profile;
using SushiShop.Core.Resources;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Profile
{
    public class AuthorizationViewModel : BasePageViewModel
    {
        private readonly IProfileManager profileManager;
        private CommonInfoType commonInfoType;

        public AuthorizationViewModel(IProfileManager profileManager)
        {
            this.profileManager = profileManager;

            SignInCommand = new SafeAsyncCommand(ExecutionStateWrapper, SignInAsync);
            SignUpCommand = new SafeAsyncCommand(ExecutionStateWrapper, SignUpAsync);
        }

        public string Title => GetTitle();

        private string phoneOrEmail;
        public string PhoneOrEmail
        {
            get => phoneOrEmail;
            set => SetProperty(ref phoneOrEmail, value);
        }

        private bool isExistLogin;
        public bool IsExistLogin
        {
            get => isExistLogin;
            set => SetProperty(ref isExistLogin, value);
        }

        public IMvxCommand SignInCommand { get; }
        public IMvxCommand SignUpCommand { get; }

        private Task SignInAsync()
        {
            return NavigationManager.NavigateAsync<AcceptPhoneViewModel>();
        }

        private Task SignUpAsync()
        {
            return NavigationManager.NavigateAsync<RegistrationViewModel>();
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
