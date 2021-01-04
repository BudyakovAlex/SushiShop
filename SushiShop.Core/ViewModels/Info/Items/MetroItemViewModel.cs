using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SushiShop.Core.ViewModels.Info.Items
{
    public class MetroItemViewModel : BaseViewModel
    {
        public MetroItemViewModel()
        {
            GoToShopsCommand = new MvxAsyncCommand(GoToShopsAsync);
        }

        public ICommand GoToShopsCommand { get; }

        public string Name { get; }

        public long Id { get; }

        private Task GoToShopsAsync()
        {
            return NavigationManager.NavigateAsync<ShopsNearMetroViewModel>();
        }
    }
}
