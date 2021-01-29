namespace SushiShop.Core.Data.Models.Orders
{
    public class OrderConfirmed
    {
        public OrderConfirmed(long orderNumber, OrderConfirmationInfo confirmationInfo)
        {
            OrderNumber = orderNumber;
            ConfirmationInfo = confirmationInfo;
        }

        public long OrderNumber { get; }

        public OrderConfirmationInfo ConfirmationInfo { get; }
    }
}