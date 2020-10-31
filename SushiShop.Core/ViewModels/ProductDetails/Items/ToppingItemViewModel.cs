using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
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
            StepperViewModel = new StepperViewModel(0, (count) => topping.CountInBasket = count);
        }

        public string Title { get; }
        public long Price { get; }

        public StepperViewModel StepperViewModel { get; }

        public void Reset()
        {
            StepperViewModel.Reset();
        }
    }
}