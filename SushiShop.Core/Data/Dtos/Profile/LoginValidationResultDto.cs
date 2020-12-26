using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Profile
{
    public class LoginValidationResultDto
    {
        [JsonProperty("authByPhone")]
        public bool IsAuthByPhone { get; set; }

        [JsonProperty("authByEmail")]
        public bool IsAuthByEmail { get; set; }

        [JsonProperty("needRegistration")]
        public bool IsRegistrationNeeded { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("isPhone")]
        public bool IsPhone { get; set; }

        [JsonProperty("isEmail")]
        public bool IsEmail { get; set; }
    }
}