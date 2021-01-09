using BuildApps.Core.Mobile.MvvmCross.ViewModels.Simple;
using MvvmCross.Commands;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SushiShop.Core.ViewModels.Shops.Items
{
    public class MetroItemViewModel : TextItemViewModel
    {
        private readonly Func<MetroItemViewModel, Task> goToShopsFunc;

        public MetroItemViewModel(string name, Func<MetroItemViewModel, Task> goToShopsFunc) : base(name)
        {
            this.goToShopsFunc = goToShopsFunc;

            GoToShopsCommand = new MvxAsyncCommand(GoToShopsAsync);
        }

        private Task GoToShopsAsync()
        {
            return goToShopsFunc?.Invoke(this) ?? Task.CompletedTask;
        }

        public ICommand GoToShopsCommand { get; }
    }
}