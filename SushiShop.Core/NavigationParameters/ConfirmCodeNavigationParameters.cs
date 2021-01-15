namespace SushiShop.Core.NavigationParameters
{
    public class ConfirmCodeNavigationParameters
    {
        public ConfirmCodeNavigationParameters(
            string login,
            string? message,
            string? placeholder)
        {
            Login = login;
            Message = message;
            Placeholder = placeholder;
        }

        public string Login { get; }

        public string? Message { get; }

        public string? Placeholder { get; }
    }
}