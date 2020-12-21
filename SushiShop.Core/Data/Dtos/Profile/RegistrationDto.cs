using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Profile
{
    public class RegistrationDto
    {
        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("phone")]
        public string? Phone { get; set; }
    }
}