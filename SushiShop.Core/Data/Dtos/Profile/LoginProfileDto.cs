using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Profile
{
    public class LoginProfileDto
    {
        [JsonProperty("authByEmail")]
        public bool IsAuthByEmail { get; set; }

        [JsonProperty("authByPhone")]
        public bool IsAuthByPhone { get; set; }

        [JsonProperty("isEmail")]
        public bool IsEmail { get; set; }

        [JsonProperty("isPhone")]
        public bool IsPhone { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("needRegistration")]
        public bool IsNeedRegistration { get; set; }

        [JsonProperty("phone")]
        public string? Phone { get; set; }
    }
}