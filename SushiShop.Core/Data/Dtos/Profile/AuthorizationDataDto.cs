using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Profile
{
    public class AuthorizationDataDto
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("isValidUser")]
        public bool IsValidUser { get; set; }

        [JsonProperty("token")]
        public TokenDto Token { get; set; }
    }
}