using SushiShop.Core.Data.Dtos.Profile;

namespace SushiShop.Core.Data.Models.Profile
{
    public class AuthProfile
    {
        public AuthProfile(bool authByEmail, TokenDto? token, int userId)
        {
            AuthByEmail = authByEmail;
            Token = token;
            UserId = userId;
        }

        public bool AuthByEmail { get; set; }

        public TokenDto? Token { get; set; }

        public int UserId { get; set; }
    }
}