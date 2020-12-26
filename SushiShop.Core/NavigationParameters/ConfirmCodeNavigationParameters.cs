namespace SushiShop.Core.NavigationParameters
{
    public class ConfirmCodeNavigationParameters
    {
        public ConfirmCodeNavigationParameters(string login, string? message)
        {
            Login = login;
            Message = message;
        }

        public string Login { get; }

        public string? Message { get; }
    }
}