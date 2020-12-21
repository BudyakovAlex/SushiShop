﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SushiShop.Core.Data.Dtos.Profile
{
    public class DiscountDto
    {
        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("phone")]
        public string? Phone { get; set; }

        [JsonProperty("balanceToNextLevel")]
        public int BalanceToNextLevel { get; set; }

        [JsonProperty("discount")]
        public int Discount { get; set; }

        [JsonProperty("certificate")]
        public int Certificate { get; set; }

        [JsonProperty("bonuses")]
        public int Bonuses { get; set; }
    }
}