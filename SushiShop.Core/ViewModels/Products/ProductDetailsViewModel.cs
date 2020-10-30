using System;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.ViewModels.Common;
using SushiShop.Core.ViewModels.Menu.Items;

namespace SushiShop.Core.ViewModels.Products
{
    public class ProductDetailsViewModel : BaseItemsPageViewModel<ProductItemViewModel>
    {
        public string OldPrice { get; set; } = "259₽";

        public StepperViewModel StepperViewModel { get; }

        public ProductDetailsViewModel()
        {
            StepperViewModel = new StepperViewModel(0, _ => { });

            Items = new MvvmCross.ViewModels.MvxObservableCollection<ProductItemViewModel>()
            {
                new ProductItemViewModel(new Data.Models.Products.Product(0, "Сушинка 0", null, 1, DateTime.Now, DateTime.Now, null, DateTime.Now, null, null, new Data.Models.Common.Currency(null, "", 1), 234, 190, null, null, 2)),
                new ProductItemViewModel(new Data.Models.Products.Product(0, "Сушинка 1", null, 1, DateTime.Now, DateTime.Now, null, DateTime.Now, null, null, new Data.Models.Common.Currency(null, "", 1), 234, 190, null, null, 2)),
                new ProductItemViewModel(new Data.Models.Products.Product(0, "Сушинка 2", null, 1, DateTime.Now, DateTime.Now, null, DateTime.Now, null, null, new Data.Models.Common.Currency(null, "", 1), 234, 190, null, null, 2)),
                new ProductItemViewModel(new Data.Models.Products.Product(0, "Сушинка 3", null, 1, DateTime.Now, DateTime.Now, null, DateTime.Now, null, null, new Data.Models.Common.Currency(null, "", 1), 234, 190, null, null, 2)),
                new ProductItemViewModel(new Data.Models.Products.Product(0, "Сушинка 4", null, 1, DateTime.Now, DateTime.Now, null, DateTime.Now, null, null, new Data.Models.Common.Currency(null, "", 1), 234, 190, null, null, 2)),
                new ProductItemViewModel(new Data.Models.Products.Product(0, "Сушинка 5", null, 1, DateTime.Now, DateTime.Now, null, DateTime.Now, null, null, new Data.Models.Common.Currency(null, "", 1), 234, 190, null, null, 2)),
                new ProductItemViewModel(new Data.Models.Products.Product(0, "Сушинка 6", null, 1, DateTime.Now, DateTime.Now, null, DateTime.Now, null, null, new Data.Models.Common.Currency(null, "", 1), 234, 190, null, null, 2)),
                new ProductItemViewModel(new Data.Models.Products.Product(0, "Сушинка 7", null, 1, DateTime.Now, DateTime.Now, null, DateTime.Now, null, null, new Data.Models.Common.Currency(null, "", 1), 234, 190, null, null, 2))
            };
        }
    }
}
