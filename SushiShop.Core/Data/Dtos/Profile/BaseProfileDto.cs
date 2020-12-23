using Newtonsoft.Json;
using SushiShop.Core.Data.Enums;
using System;

namespace SushiShop.Core.Data.Dtos.Profile
{
    public class BaseProfileDto
    {
        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("phone")]
        public string? Phone { get; set; }

        [JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty("firstName")]
        public string? FirstName { get; set; }

        [JsonProperty("lastName")]
        public string? LastName { get; set; }

        [JsonProperty("fullName")]
        public string? FullName { get; set; }

        [JsonProperty("gender")]
        public GenderType? Gender { get; set; }

        [JsonProperty("allowSubscribe")]
        public bool IsAllowSubscribe { get; set; }

        [JsonProperty("allowNotifications")]
        public bool IsAllowNotifications { get; set; }

        [JsonProperty("allowPush")]
        public bool IsAllowPush { get; set; }

        [JsonProperty("needRegistration")]
        public bool IsNeedRegistration { get; set; }
    }
}