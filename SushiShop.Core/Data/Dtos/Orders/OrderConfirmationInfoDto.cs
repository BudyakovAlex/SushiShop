namespace SushiShop.Core.Data.Dtos.Orders
{
    public class OrderConfirmationInfoDto
    {
        public string? Image { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? OrderNumberTitle { get; set; }
        public string? PaymentUrl { get; set; }
    }
}