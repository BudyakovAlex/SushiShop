using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Simple;
using MvvmCross.Commands;
using SushiShop.Core.Data.Models.Toppings;

namespace SushiShop.Core.ViewModels.CardProduct.Items
{
    public class ToppingItemViewModel : BaseViewModel
    {
        public ToppingItemViewModel(Topping topping)
        {
            Title = topping.PageTitle ?? string.Empty;
            Price = topping.Price;
            CountInBusket = topping.CountInBasket;

            IncrementCommand = new MvxCommand(() => IncrementAsync());
            DecrementCommand = new MvxCommand(() => DecrementAsync());
        }

        public IMvxCommand IncrementCommand { get; }
        public IMvxCommand DecrementCommand { get; }

        public string Title { get; }
        public long Price { get; }
        public long CountInBusket { get; }

        private Task IncrementAsync()
        {
            return Task.CompletedTask;
        }

        private Task DecrementAsync()
        {
            return Task.CompletedTask;
        }
    }
}