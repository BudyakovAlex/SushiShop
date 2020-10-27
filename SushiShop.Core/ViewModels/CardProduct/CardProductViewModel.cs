using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Managers.Products;
using SushiShop.Core.Resources;

namespace SushiShop.Core.ViewModels.CardProduct
{
    public class CardProductViewModel : BasePageViewModel
    {
        private readonly IProductsManager productsManager;

        public CardProductViewModel(IProductsManager productsManager)
        {
            this.productsManager = productsManager;

            AddToCartCommand = new SafeAsyncCommand(ExecutionStateWrapper, AddToCartAsync);
        }

        public IMvxCommand AddToCartCommand { get; }

        public string ProteinTitle => GetProteinTitle();
        public string FatsTitle => GetFatsTitle();
        public string CarbohydratesTitle => GetCarbohydratesTitle();
        public string Ccal => GetCcalTitle();

        private string protein;
        public string Protein
        {
            get => protein;
            set => SetProperty(ref protein, value);
        }

        private string fats;
        public string Fats
        {
            get => fats;
            set => SetProperty(ref fats, value);
        }

        private string carbohydrates;
        public string Carbohydrates
        {
            get => carbohydrates;
            set => SetProperty(ref carbohydrates, value);
        }

        private string ccal;
        public string Ccal
        {
            get => ccal;
            set => SetProperty(ref ccal, value);
        }

        private async Task AddToCartAsync()
        {

        }
        private string GetProteinTitle()
        {
            return AppStrings.Protein;
        }

        private string GetFatsTitle()
        {
            return AppStrings.Fats;
        }

        private string GetCarbohydratesTitle()
        {
            return AppStrings.Carbohydrates;
        }

        private string GetCcalTitle()
        {
            return AppStrings.Ccal;
        }
    }
}