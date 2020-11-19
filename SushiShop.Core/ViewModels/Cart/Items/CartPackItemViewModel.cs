using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Models.Cart;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.ViewModels.Common;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Cart.Items
{
    public class CartPackItemViewModel : BaseViewModel
    {
        private readonly ICartManager cartManager;
        private readonly Packaging pack;
        private readonly string? city;

        public CartPackItemViewModel(
            ICartManager cartManager,
            Packaging pack,
            string? city)
        {
            this.cartManager = cartManager;
            this.pack = pack;
            this.city = city;

            StepperViewModel = new StepperViewModel(pack.CountInBasket, OnCountChangedAsync);
        }

        public StepperViewModel StepperViewModel { get; }

        public string? Title => pack?.PageTitle;

        public string Price => $"{pack?.Price} {pack.Currency.Symbol}";
        public string? OldPrice => pack?.OldPrice == 0 ? null : $"{pack?.OldPrice} {pack?.Currency?.Symbol}";
        public int? CountInBasket => pack?.CountInBasket;

        private async Task OnCountChangedAsync(int count)
        {
            //var response = await cartManager.UpdateProductInCartAsync(city, product!.Id, product?.Uid, count, Array.Empty<Packaging>()); 
            //if (response.Data is null)
            //{
            //    return;
            //}

            //product!.Uid = response.Data.Uid;
        }
    }
}
