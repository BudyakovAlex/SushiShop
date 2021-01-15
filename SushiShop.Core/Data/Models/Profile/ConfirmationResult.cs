namespace SushiShop.Core.Data.Models.Profile
{
    public class ConfirmationResult
    {
        public ConfirmationResult(
            string message,
            string phone,
            string placeholder)
        {
            Message = message;
            Phone = phone;
            Placeholder = placeholder;
        }

        public string Message { get; }

        public string Placeholder { get; }

        public string Phone { get; }
    }
}