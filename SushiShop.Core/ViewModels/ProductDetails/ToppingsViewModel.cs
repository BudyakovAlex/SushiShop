using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using MvvmCross.Commands;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.Extensions;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.ViewModels.CardProduct.Items;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.ProductDetails
{
    public class ToppingsViewModel : BaseItemsPageViewModel<ToppingItemViewModel, ToppingNavigationParameters, List<Topping>>
    {
        private List<Topping> toppings;

        public ToppingsViewModel()
        {
            toppings = new List<Topping>();

            AddToCartCommand = new SafeAsyncCommand(ExecutionStateWrapper, AddToCartAsync);
            ResetCommand = new MvxCommand(Reset);
        }

        public IMvxCommand AddToCartCommand { get; }

        public IMvxCommand ResetCommand { get; }

        public string? PageTitle { get; private set; }

        public override void Prepare(ToppingNavigationParameters parameter)
        {
            toppings = parameter.Toppings;
            PageTitle = parameter.Title;

            var viewModels = parameter.Toppings.Select(topping => new ToppingItemViewModel(topping, parameter.Currency)).ToList();
            Items.AddRange(viewModels);
        }

        private Task AddToCartAsync()
        {
            return NavigationManager.CloseAsync(this, toppings);
        }

        private void Reset()
        {
            Items.ForEach(item => item.Reset());
        }
    }
}