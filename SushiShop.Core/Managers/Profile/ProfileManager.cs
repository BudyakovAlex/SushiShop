using System;
using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Profile;
using SushiShop.Core.Mappers;
using SushiShop.Core.Providers;
using SushiShop.Core.Services.Http.Profile;

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

        public async Task<Response<LoginValidationResult?>> CheckIsLoginAvailableAsync(string? login)
        {
            var response = await profileService.CheckIsLoginAvailableAsync(login, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<LoginValidationResult?>(isSuccessful: true, data);
            }

            return new Response<LoginValidationResult?>(isSuccessful: false, null, response.Data?.Errors ?? Array.Empty<string>());
        }

        public async Task<Response<LoginValidationResult?>> SendCodeAsync(string? login)
        {
            var response = await profileService.SendCodeAsync(login, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<LoginValidationResult?>(isSuccessful: true, data);
            }

            return new Response<LoginValidationResult?>(isSuccessful: false, null, response.Data?.Errors ?? Array.Empty<string>());
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

            return new Response<AuthorizationData?>(isSuccessful: false, null, response.Data?.Errors ?? Array.Empty<string>());
        }

        public async Task<Response<ConfirmationResult?>> RegistrationAsync(Data.Models.Profile.Profile profile)
        {
            var response = await profileService.RegistrationAsync(profile.Map(), CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<ConfirmationResult?>(isSuccessful: true, data);
            }

            return new Response<ConfirmationResult?>(isSuccessful: false, null, response.Data?.Errors ?? Array.Empty<string>());
        }

        public async Task<Response<DetailedProfile?>> GetProfileAsync()
        {
            var response = await profileService.GetProfileAsync(CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<DetailedProfile?>(isSuccessful: true, data);
            }

            return new Response<DetailedProfile?>(isSuccessful: false, null, response.Data?.Errors ?? Array.Empty<string>());
        }

        public async Task<Response<ProfileDiscount?>> GetDiscountAsync()
        {
            var response = await profileService.GetDiscountAsync(CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<ProfileDiscount?>(isSuccessful: true, data);
            }

            return new Response<ProfileDiscount?>(isSuccessful: false, null, response.Data?.Errors ?? Array.Empty<string>());
        }

        public async Task<Response<DetailedProfile?>> SaveProfileAsync(Data.Models.Profile.Profile profile)
        {
            var response = await profileService.SaveProfileAsync(profile.Map(), CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<DetailedProfile?>(isSuccessful: true, data);
            }

            return new Response<DetailedProfile?>(isSuccessful: false, null, response.Data?.Errors ?? Array.Empty<string>());
        }

        public async Task<Response<string?>> UploadPhotoAsync(string imagePath)
        {
            var response = await profileService.UploadPhotoAsync(imagePath, CancellationToken.None);
            if (response.IsSuccessful)
            {
                return new Response<string?>(isSuccessful: true, response.Data!.SuccessData);
            }

            return new Response<string?>(isSuccessful: false, null, response.Data?.Errors ?? Array.Empty<string>());
        }
    }
}