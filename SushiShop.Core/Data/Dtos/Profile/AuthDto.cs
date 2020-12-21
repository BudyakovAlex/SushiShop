using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Profile
{
    public class AuthDto
    {
        [JsonProperty("isValidUser")]
        public bool AuthByEmail { get; set; }

        [JsonProperty("token")]
        public TokenDto? Token { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }
    }
}
