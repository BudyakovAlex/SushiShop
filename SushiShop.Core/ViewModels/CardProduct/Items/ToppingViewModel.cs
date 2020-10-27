using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.NavigationParameters;

namespace SushiShop.Core.ViewModels.CardProduct.Items
{
    public class ToppingViewModel : BasePageViewModel<ToppingNavigationParameters>
    {
        public ToppingViewModel(Topping? topping)
        {
            Title = topping?.PageTitle;
            Price = topping.Price;
            CountInBusket = topping.CountInBasket;

            IncrementCommand = new MvxCommand(() => IncrementAsync());
            DecrementCommand = new MvxCommand(() => DecrementAsync());
        }

        public IMvxCommand IncrementCommand { get; }
        public IMvxCommand DecrementCommand { get; }

        public string? Title { get; private set; }
        public long Price { get; private set; }
        public long CountInBusket { get; private set; }

        private Task IncrementAsync()
        {
            return Task.CompletedTask;
        }

        private Task DecrementAsync()
        {
            return Task.CompletedTask;
        }

        public override void Prepare(ToppingNavigationParameters parameter)
        {
            Title = parameter.Topping.PageTitle;
            Price = parameter.Topping.Price;
            CountInBusket = parameter.Topping.CountInBasket;
        }
    }
}