using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Profile
{
    public class AuthorizationDataDto
    {
        [JsonProperty("isValidUser")]
        public bool IsValidUser { get; set; }

        [JsonProperty("token")]
        public TokenDto? Token { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }
    }
}