using System;
using System.Collections.Generic;
using System.Text;

namespace SushiShop.Core.Data.Models.Profile
{
    class RegistrationProfile
    {
        public RegistrationProfile(string? message, string? phone)
        {
            Message = message;
            Phone = phone;
        }

        public string? Message { get; set; }

        public string? Phone { get; set; }
    }
}
