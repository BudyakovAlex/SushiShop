namespace SushiShop.Core.Data.Models.Cities
{
    public class AvailableReceiveMethods
    {
        public AvailableReceiveMethods(
            bool delivery,
            bool pickup)
        {
            CanDelivery = delivery;
            CanPickup = pickup;
        }

        public bool CanDelivery { get; }

        public bool CanPickup { get; }
    }
}
