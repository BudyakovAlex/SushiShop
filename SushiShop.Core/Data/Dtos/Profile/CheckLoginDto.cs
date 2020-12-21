using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Profile
{
    public class CheckLoginDto
    {
        [JsonProperty("authByEmail")]
        public bool AuthByEmail { get; set; }

        [JsonProperty("authByPhone")]
        public bool AuthByPhone { get; set; }

        [JsonProperty("isEmail")]
        public bool IsEmail { get; set; }

        [JsonProperty("isPhone")]
        public bool IsPhone { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("needRegistration")]
        public bool NeedRegistration { get; set; }

        [JsonProperty("phone")]
        public string? Phone { get; set; }
    }
}
