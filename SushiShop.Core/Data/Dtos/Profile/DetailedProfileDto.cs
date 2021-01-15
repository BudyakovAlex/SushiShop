using Newtonsoft.Json;
using SushiShop.Core.Data.Dtos.Common;

namespace SushiShop.Core.Data.Dtos.Profile
{
    public class DetailedProfileDto : ProfileDto
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("dateOfBirthFormated")]
        public string? DateOfBirthFormated { get; set; }

        [JsonProperty("dateOfBirthReadonly")]
        public bool CanChangeDateOfBirth { get; set; }

        [JsonProperty("phoneReadonly")]
        public bool CanChangePhone { get; set; }

        [JsonProperty("photo")]
        public ImageInfoDto? Photo { get; set; }
    }
}