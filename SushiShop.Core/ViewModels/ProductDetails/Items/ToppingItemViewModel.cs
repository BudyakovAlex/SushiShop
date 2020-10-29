using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
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
        public long CountInBusket { get; private set; }

        private void IncrementAsync()
        {
            CountInBusket++;
        }

        private void DecrementAsync()
        {
            if(CountInBusket > 1)
                CountInBusket--; 
        }
    }
}