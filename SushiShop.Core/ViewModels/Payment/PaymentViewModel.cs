using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SushiShop.Core.ViewModels.Payment
{
    public class PaymentViewModel : BasePageViewModel<string, bool>
    {
        public PaymentViewModel()
        {
            ConfirmPaymentCommand = new SafeAsyncCommand(ExecutionStateWrapper, ConfirmPaymentAsync);
        }

        public ICommand ConfirmPaymentCommand { get; }

        public string? PaymentUrl { get; private set; }

        public override void Prepare(string parameter)
        {
            PaymentUrl = parameter;
        }

        private Task ConfirmPaymentAsync()
        {
            return NavigationManager.CloseAsync(this, true);
        }
    }
}