namespace SushiShop.Core.Data.Models.Profile
{
    public class LoginProfile
    {
        public LoginProfile(
            bool isAuthByEmail,
            bool isAuthByPhone,
            bool isEmail,
            bool isPhone,
            string? message,
            bool isNeedRegistration,
            string? phone)
        {
            IsAuthByEmail = isAuthByEmail;
            IsAuthByPhone = isAuthByPhone;
            IsEmail = isEmail;
            IsPhone = isPhone;
            Message = message;
            IsNeedRegistration = isNeedRegistration;
            Phone = phone;
        }

        public bool IsAuthByEmail { get; set; }
        public bool IsAuthByPhone { get; set; }
        public bool IsEmail { get; set; }
        public bool IsPhone { get; set; }
        public string? Message { get; set; }
        public bool IsNeedRegistration { get; set; }
        public string? Phone { get; set; }
    }
}