﻿using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Profile;
using SushiShop.Core.Mappers;
using SushiShop.Core.Providers;
using SushiShop.Core.Services.Http.Profile;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.Profile
{
    public class ProfileManager : IProfileManager
    {
        private readonly IProfileService profileService;
        private readonly IUserSession userSession;

        public ProfileManager(IProfileService profileService, IUserSession userSession)
        {
            this.profileService = profileService;
            this.userSession = userSession;
        }

        public async Task<Response<Data.Models.Profile.Profile?>> CheckIsLoginAvailableAsync(string? login, bool? shouldSendCode)
        {
            var response = await profileService.CheckIsLoginAvailableAsync(login, shouldSendCode, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<Data.Models.Profile.Profile?>(isSuccessful: true, data);
            }

            return new Response<Data.Models.Profile.Profile?>(isSuccessful: false, null);
        }

        public async Task<Response<AuthorizationData?>> AuthorizeAsync(string login, string pass)
        {
            var response = await profileService.AuthorizeAsync(login, pass, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                userSession.SetToken(data!.Token);
                return new Response<AuthorizationData?>(isSuccessful: true, data);
            }

            return new Response<AuthorizationData?>(isSuccessful: false, null);
        }

        public async Task<Response<ConfirmationResult?>> RegistrationAsync(Data.Models.Profile.Profile profile)
        {
            var response = await profileService.RegistrationAsync(profile.Map(), CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<ConfirmationResult?>(isSuccessful: true, data);
            }

            return new Response<ConfirmationResult?>(isSuccessful: false, null);
        }

        public async Task<Response<Data.Models.Profile.DetailedProfile?>> GetProfileAsync()
        {
            var response = await profileService.GetProfileAsync(CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<Data.Models.Profile.DetailedProfile?>(isSuccessful: true, data);
            }

            return new Response<Data.Models.Profile.DetailedProfile?>(isSuccessful: false, null);
        }

        public async Task<Response<ProfileDiscount?>> GetDiscountAsync()
        {
            var response = await profileService.GetDiscountAsync(CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<ProfileDiscount?>(isSuccessful: true, data);
            }

            return new Response<ProfileDiscount?>(isSuccessful: false, null);
        }

        public async Task<Response<Data.Models.Profile.DetailedProfile?>> SaveProfileAsync(Data.Models.Profile.Profile profile)
        {
            var response = await profileService.SaveProfileAsync(profile.Map(), CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<Data.Models.Profile.DetailedProfile?>(isSuccessful: true, data);
            }

            return new Response<Data.Models.Profile.DetailedProfile?>(isSuccessful: false, null);
        }
    }
}