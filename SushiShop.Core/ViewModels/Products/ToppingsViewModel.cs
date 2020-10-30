using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.ViewModels.Products.Items;

namespace SushiShop.Core.ViewModels.Products
{
    public class ToppingsViewModel : BaseItemsPageViewModel<ToppingItemViewModel>
    {
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ToppingsViewModel()
        {
            Title = "Добавить соус";

            Items = new MvvmCross.ViewModels.MvxObservableCollection<ToppingItemViewModel>()
            {
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽"),
                new ToppingItemViewModel("Чили сладкий 30г", "30₽")
            };
        }
    }
}
