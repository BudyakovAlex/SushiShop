using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.ViewModels.Common;
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

        private string _buttonText;
        public string ButtonText
        {
            get => _buttonText;
            set => SetProperty(ref _buttonText, value);
        }

        public ToppingsViewModel()
        {
            Title = "Добавить соус";
            ButtonText = "В корзину";
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
