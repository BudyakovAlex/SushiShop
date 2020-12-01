namespace SushiShop.Core.NavigationParameters
{
    public class CardProductNavigationParameters
    {
        public CardProductNavigationParameters(long id, string? city, bool isReadonly = false)
        {
            Id = id;
            City = city;
            IsReadonly = isReadonly;
        }

        public long Id { get; }

        public string? City { get; }

        public bool IsReadonly { get; }
    }
}