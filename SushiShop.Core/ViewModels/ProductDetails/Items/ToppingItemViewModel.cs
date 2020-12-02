using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.ViewModels.Common;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.CardProduct.Items
{
    public class ToppingItemViewModel : BaseViewModel
    {
        private readonly Topping topping;

        public ToppingItemViewModel(Topping topping, string? currency)
        {
            this.topping = topping;

            Title = topping.PageTitle ?? string.Empty;
            Price = $"{topping.Price} {currency}";

            StepperViewModel = new StepperViewModel(topping.CountInBasket, OnCountChangedAsync);
        }

        public string Title { get; }

        public string Price { get; }

        public StepperViewModel StepperViewModel { get; }

        public void Reset()
        {
            StepperViewModel.Reset();
            topping.CountInBasket = 0;
        }

        private Task OnCountChangedAsync(int previousCount, int newCount)
        {
            topping.CountInBasket = newCount;
            return Task.CompletedTask;
        }
    }
}