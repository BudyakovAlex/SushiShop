using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using MvvmCross.Commands;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.ViewModels.CardProduct.Items;

namespace SushiShop.Core.ViewModels.ProductDetails
{
    public class ToppingsViewModel : BaseItemsPageViewModel<ToppingItemViewModel, ToppingNavigationParameters, List<Topping>>
    {
        private List<Topping> toppings;

        public ToppingsViewModel()
        {
            AddToCartCommand = new SafeAsyncCommand(ExecutionStateWrapper, AddToCartAsync);
        }

        public IMvxCommand AddToCartCommand { get; }

        public override void Prepare(ToppingNavigationParameters parameter)
        {
            toppings = parameter.Toppings;
            Items.AddRange(parameter.Toppings.Select(topping => new ToppingItemViewModel(topping)));
        }

        private async Task AddToCartAsync()
        {
            await NavigationManager.CloseAsync(this, toppings);
        }
    }
}