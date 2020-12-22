namespace SushiShop.Core.Data.Models.Profile
{
    public class AuthorizationData
    {
        public AuthorizationData(int userId, bool isValidUser, Token? token)
        {
            UserId = userId;
            IsValidUser = isValidUser;
            Token = token;
        }

        public int UserId { get; }

        public bool IsValidUser { get; }

        public Token? Token { get; }
    }
}