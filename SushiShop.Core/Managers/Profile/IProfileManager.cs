﻿using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Profile;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.Profile
{
    public interface IProfileManager
    {
        Task<Response<LoginProfile>> CheckLoginAsync(string login, bool? sendCode);

        Task<Response<AuthProfile>> AuthAsync(string login, string pass);

        Task<Response<RegistrationProfile>> RegistrationAsync(RegistrationDataDto registrationDataDto);

        Task<Response<PersonalData>> GetPersonalDataAsync();

        Task<Response<ProfileDiscount>> GetDiscountAsync();
    }
}