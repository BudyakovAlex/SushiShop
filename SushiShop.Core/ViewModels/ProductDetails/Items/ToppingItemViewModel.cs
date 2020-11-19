using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.ViewModels.Common;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.CardProduct.Items
{
    public class ToppingItemViewModel : BaseViewModel
    {
        private readonly Topping topping;

        public ToppingItemViewModel(Topping topping)
        {
            this.topping = topping;
            Title = topping.PageTitle ?? string.Empty;
            Price = topping.Price;

            StepperViewModel = new StepperViewModel(topping.CountInBasket, OnCountChangedAsync);
        }

        public string Title { get; }

        public decimal Price { get; }

        public StepperViewModel StepperViewModel { get; }

        public void Reset()
        {
            StepperViewModel.Reset();
        }

        private Task OnCountChangedAsync(int previousCount, int newCount)
        {
            topping.CountInBasket = newCount;
            return Task.CompletedTask;
        }
    }
}