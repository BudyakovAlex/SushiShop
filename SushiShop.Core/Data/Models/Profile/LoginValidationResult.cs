namespace SushiShop.Core.Data.Models.Profile
{
    public class LoginValidationResult
    {
        public LoginValidationResult(
            bool isAuthByPhone,
            bool isAuthByEmail,
            bool isRegistrationNeeded,
            string? message,
            string? placeholder,
            bool isPhone,
            bool isEmail)
        {
            IsAuthByPhone = isAuthByPhone;
            IsAuthByEmail = isAuthByEmail;
            IsRegistrationNeeded = isRegistrationNeeded;
            Message = message;
            Placeholder = placeholder;
            IsPhone = isPhone;
            IsEmail = isEmail;
        }

        public bool IsAuthByPhone { get; }

        public bool IsAuthByEmail { get; }

        public bool IsRegistrationNeeded { get; }

        public string? Message { get; }

        public string? Placeholder { get; }

        public bool IsPhone { get; }

        public bool IsEmail { get; }
    }
}