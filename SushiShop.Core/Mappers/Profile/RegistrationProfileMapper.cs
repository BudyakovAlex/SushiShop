using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Models.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace SushiShop.Core.Mappers.Profile
{
    public static class RegistrationProfileMapper
    {
        public static RegistrationProfile Map(this RegistrationProfileDto registrationProfileDto)
        {
            return new RegistrationProfile(registrationProfileDto.Message,
                                           registrationProfileDto.Phone);
        }
    }
}
