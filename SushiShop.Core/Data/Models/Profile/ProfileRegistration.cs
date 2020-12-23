namespace SushiShop.Core.Data.Models.Profile
{
    public class ProfileRegistration
    {
        public ProfileRegistration(string message, string phone)
        {
            Message = message;
            Phone = phone;
        }

        public string Message { get; set; }

        public string Phone { get; set; }
    }
}