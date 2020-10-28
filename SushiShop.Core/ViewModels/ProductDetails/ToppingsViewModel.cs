using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.ViewModels;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.Managers.Products;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.ViewModels.Abstract;
using SushiShop.Core.ViewModels.CardProduct.Items;
using SushiShop.Core.ViewModels.Cities.Items;

namespace SushiShop.Core.ViewModels.ProductDetails
{
    public class ToppingsViewModel : BasePageViewModel<ToppingNavigationParameters, List<Topping>>
    {
        public ToppingsViewModel()
        {
            Items = new MvxObservableCollection<ToppingItemViewModel>();
        }

        public MvxObservableCollection<ToppingItemViewModel> Items { get; }

        public override void Prepare(ToppingNavigationParameters parameter)
        {
            var items = parameter.Toppings.Select(topping => new ToppingItemViewModel(topping)).ToList();
            Items.AddRange(items);
        }
    }
}