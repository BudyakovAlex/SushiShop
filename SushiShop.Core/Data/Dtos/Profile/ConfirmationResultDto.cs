using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Profile
{
    public class ConfirmationResultDto
    {
        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("placeholder")]
        public string? Placeholder { get; set; }

        [JsonProperty("phone")]
        public string? Phone { get; set; }
    }
}