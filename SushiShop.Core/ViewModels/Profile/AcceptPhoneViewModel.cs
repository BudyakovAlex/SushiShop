using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Managers.Profile;

namespace SushiShop.Core.ViewModels.Profile
{
    public class AcceptPhoneViewModel : BasePageViewModel
    {
        private readonly IProfileManager profileManager;

        public AcceptPhoneViewModel(IProfileManager profileManager)
        {
            this.profileManager = profileManager;

            ContinueCommand = new SafeAsyncCommand(ExecutionStateWrapper, ContinueAsync);
        }

        private string code;
        public string Code
        {
            get => code;
            set => SetProperty(ref code, value);
        }

        public IMvxCommand ContinueCommand { get; }

        private Task ContinueAsync()
        {
            return Task.CompletedTask;
        }
    }
}
