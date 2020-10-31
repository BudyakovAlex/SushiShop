namespace SushiShop.Core.NavigationParameters
{
    public class CardProductNavigationParameters
    {
        public CardProductNavigationParameters(int id, string? city)
        {
            Id = id;
            City = city;
        }

        public int Id { get; }

        public string? City { get; }
    }
}