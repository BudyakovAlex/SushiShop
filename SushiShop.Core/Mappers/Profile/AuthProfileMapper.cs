﻿using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Models.Profile;

namespace SushiShop.Core.Mappers.Profile
{
    public static class AuthProfileMapper
    {
        public static AuthProfile Map(this AuthProfileDto authProfileDto)
        {
            return new AuthProfile(authProfileDto.AuthByEmail,
                                   authProfileDto.Token,
                                   authProfileDto.UserId);
        }
    }
}