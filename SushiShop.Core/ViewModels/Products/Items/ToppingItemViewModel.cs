using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;

namespace SushiShop.Core.ViewModels.Products.Items
{
    public class ToppingItemViewModel : BaseViewModel
    {
        public string Title { get; set; }

        public string Price { get; set; }

        public ToppingItemViewModel(string title, string price)
        {
            Title = title;
            Price = price;
        }
    }
}
