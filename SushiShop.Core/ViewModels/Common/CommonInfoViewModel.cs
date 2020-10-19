using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Enums;

namespace SushiShop.Core.ViewModels.Common
{
    public class CommonInfoViewModel : BasePageViewModel
    {
        public CommonInfoViewModel(CommonInfoType commonInfoType)
        {
        }

        public string Content { get; }
    }
}
