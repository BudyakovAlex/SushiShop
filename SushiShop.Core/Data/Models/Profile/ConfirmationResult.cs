namespace SushiShop.Core.Data.Models.Profile
{
    public class ConfirmationResult
    {
        public ConfirmationResult(string message, string phone)
        {
            Message = message;
            Phone = phone;
        }

        public string Message { get; }

        public string Phone { get; }
    }
}