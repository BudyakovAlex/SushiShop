﻿using Newtonsoft.Json;
using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Enums;

namespace SushiShop.Core.Data.Dtos.Profile
{
    public class PersonalDataDto
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("dateOfBirth")]
        public string? DateOfBirth { get; set; }

        [JsonProperty("dateOfBirthFormated")]
        public string? DateOfBirthFormated { get; set; }

        [JsonProperty("dateOfBirthReadonly")]
        public bool CanChangeDateOfBirth { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("subscribeSales")]
        public string? SubscribeSales { get; set; }

        [JsonProperty("firstName")]
        public string? FirstName { get; set; }

        [JsonProperty("lastName")]
        public string? LastName { get; set; }

        [JsonProperty("fullName")]
        public string? FullName { get; set; }

        [JsonProperty("gender")]
        public GenderType? Gender { get; set; }

        [JsonProperty("phone")]
        public string? Phone { get; set; }

        [JsonProperty("allowNotification")]
        public bool IsAllowNotification { get; set; }

        [JsonProperty("allowPush")]
        public bool IsAllowPush { get; set; }

        [JsonProperty("photo")]
        public ImageInfoDto? Photo { get; set; }
    }
}