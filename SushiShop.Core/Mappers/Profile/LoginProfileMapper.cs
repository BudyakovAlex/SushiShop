﻿using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Models.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace SushiShop.Core.Mappers.Profile
{
    public static class LoginProfileMapper
    {
        public static LoginProfile Map(this LoginProfileDto loginProfileDto)
        {
            return new LoginProfile(loginProfileDto.AuthByEmail,
                                    loginProfileDto.AuthByPhone,
                                    loginProfileDto.IsEmail,
                                    loginProfileDto.IsPhone,
                                    loginProfileDto.Message,
                                    loginProfileDto.NeedRegistration,
                                    loginProfileDto.Phone);
        }
    }
}
