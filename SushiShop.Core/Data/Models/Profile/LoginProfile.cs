namespace SushiShop.Core.Data.Models.Profile
{
    public class LoginProfile
    {
        public LoginProfile(bool authByEmail,
                     bool authByPhone,
                     bool isEmail,
                     bool isPhone,
                     string? message,
                     bool needRegistration,
                     string? phone)
        {
            AuthByEmail = authByEmail;
            AuthByPhone = authByPhone;
            IsEmail = isEmail;
            IsPhone = isPhone;
            Message = message;
            NeedRegistration = needRegistration;
            Phone = phone;
        }

        public bool AuthByEmail { get; set; }
        public bool AuthByPhone { get; set; }
        public bool IsEmail { get; set; }
        public bool IsPhone { get; set; }
        public string? Message { get; set; }
        public bool NeedRegistration { get; set; }
        public string? Phone { get; set; }
    }
}