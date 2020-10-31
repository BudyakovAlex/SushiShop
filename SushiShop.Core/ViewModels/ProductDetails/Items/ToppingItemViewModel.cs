using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.ViewModels.Common;

namespace SushiShop.Core.ViewModels.CardProduct.Items
{
    public class ToppingItemViewModel : BaseViewModel
    {
        public ToppingItemViewModel(Topping topping)
        {
            Title = topping.PageTitle ?? string.Empty;
            Price = topping.Price;
            CountInBusket = topping.CountInBasket;
            StepperViewModel = new StepperViewModel(0, _ => { });

            IncrementCommand = new MvxCommand(() => IncrementAsync());
            DecrementCommand = new MvxCommand(() => DecrementAsync());
        }

        public IMvxCommand IncrementCommand { get; }
        public IMvxCommand DecrementCommand { get; }

        public string Title { get; }
        public long Price { get; }
        public long CountInBusket { get; private set; }
        public StepperViewModel StepperViewModel { get; }

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