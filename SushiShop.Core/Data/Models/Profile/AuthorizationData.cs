using SushiShop.Core.Data.Dtos.Profile;

namespace SushiShop.Core.Data.Models.Profile
{
    public class AuthorizationData
    {
        public AuthorizationData(int userId, bool isValidUser, TokenDto? token)
        {
            UserId = userId;
            IsValidUser = isValidUser;
            Token = token;
        }

        public int UserId { get; set; }

        public bool IsValidUser { get; set; }

        public TokenDto? Token { get; set; }
    }
}