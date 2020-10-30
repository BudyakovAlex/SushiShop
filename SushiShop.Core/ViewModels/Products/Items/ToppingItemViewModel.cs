using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.ViewModels.Common;

namespace SushiShop.Core.ViewModels.Products.Items
{
    public class ToppingItemViewModel : BaseViewModel
    {
        public string Title { get; set; }

        public string Price { get; set; }

        public StepperViewModel StepperViewModel { get; }

        public ToppingItemViewModel(string title, string price)
        {
            Title = title;
            Price = price;

            StepperViewModel = new StepperViewModel(0, _ => { });
        }
    }
}
