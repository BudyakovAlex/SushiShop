using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;

namespace SushiShop.Core.ViewModels.Cart
{
    public class CartViewModel : BasePageViewModel
    {
        public CartViewModel()
        {
            CheckoutCommand = new SafeAsyncCommand(ExecutionStateWrapper, CheckoutAsync);
        }

        //public IMvxCommand AddSouceCommand { get; }
        //public IMvxCommand AddBagCommand { get; }
        public IMvxCommand CheckoutCommand { get; }

        public string Title => string.Empty;
        public string ProductUrl => string.Empty;
        public string ProductName => string.Empty;
        public string TotalPrice => string.Empty;

        private Task CheckoutAsync()
        {
            throw new System.NotImplementedException();
        }

    }
}