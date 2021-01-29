namespace SushiShop.Core.Data.Models.Orders
{
    public class OrderConfirmationInfo
    {
        public OrderConfirmationInfo(
            string? image,
            string? title,
            string? content,
            string? orderNumberTitle,
            string? paymentUrl)
        {
            Image = image;
            Title = title;
            Content = content;
            OrderNumberTitle = orderNumberTitle;
            PaymentUrl = paymentUrl;
        }

        public string? Image { get; }

        public string? Title { get; }

        public string? Content { get; }

        public string? OrderNumberTitle { get; }

        public string? PaymentUrl { get; }
    }
}
