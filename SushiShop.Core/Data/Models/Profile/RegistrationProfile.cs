namespace SushiShop.Core.Data.Models.Profile
{
    internal class RegistrationProfile
    {
        public RegistrationProfile(string? message, string? phone)
        {
            Message = message;
            Phone = phone;
        }

        public string? Message { get; set; }

        public string? Phone { get; set; }
    }
}