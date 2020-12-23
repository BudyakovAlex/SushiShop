namespace SushiShop.Core.NavigationParameters
{
    public class RegistrationNavigationParameters
    {
        public RegistrationNavigationParameters(string login)
        {
            Login = login;
        }

        public string Login { get; }
    }
}
