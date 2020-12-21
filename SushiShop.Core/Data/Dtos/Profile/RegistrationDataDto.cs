using Newtonsoft.Json;
using SushiShop.Core.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SushiShop.Core.Data.Dtos.Profile
{
    public class RegistrationDataDto
    {
        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("phone")]
        public string? Phone { get; set; }

        [JsonProperty("dateOfBirth")]
        public string? DateOfBirth { get; set; }

        [JsonProperty("firstName")]
        public string? FirstName { get; set; }

        [JsonProperty("lastName")]
        public string? LastName { get; set; }

        [JsonProperty("fullName")]
        public string? FullName { get; set; }

        [JsonProperty("gender")]
        public GenderType? Gender { get; set; }

        [JsonProperty("allowSubscribe")]
        public bool AllowSubscribe { get; set; }

        [JsonProperty("allowNotificarions")]
        public bool AllowNotificarions { get; set; }

        [JsonProperty("allowPush")]
        public bool AllowPush { get; set; }
    }
}
