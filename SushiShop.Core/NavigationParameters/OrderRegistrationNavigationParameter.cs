namespace SushiShop.Core.NavigationParameters
{
    public class OrderRegistrationNavigationParameter
    {
        public OrderRegistrationNavigationParameter(
            Data.Models.Cart.Cart cart,
            Data.Models.Cities.AvailableReceiveMethods availableReceiveMethods)
        {
            Cart = cart;
            AvailableReceiveMethods = availableReceiveMethods;
        }

        public Data.Models.Cart.Cart Cart { get; }

        public Data.Models.Cities.AvailableReceiveMethods AvailableReceiveMethods { get; }
    }
}
