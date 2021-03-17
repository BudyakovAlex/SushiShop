namespace SushiShop.Core.NavigationParameters
{
    public class CardProductNavigationParameters
    {
        public CardProductNavigationParameters(
            long id,
            string? city,
            bool isReadonly = false,
            bool isCartProduct = false)
        {
            Id = id;
            City = city;
            IsReadonly = isReadonly;
            IsCartProduct = isCartProduct;
        }

        public long Id { get; }

        public string? City { get; }

        public bool IsReadonly { get; }

        public bool IsCartProduct { get; }
    }
}