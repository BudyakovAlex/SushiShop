using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Profile;
using SushiShop.Core.Mappers;
using SushiShop.Core.Services.Http.Profile;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.Profile
{
    public class ProfileManager : IProfileManager
    {
        private readonly IProfileService profileService;

        public ProfileManager(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        public async Task<Response<BaseProfile>> CheckIsLoginAvailableAsync(string login, bool? sendCode)
        {
            var response = await profileService.CheckIsLoginAvailableAsync(login, sendCode, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<BaseProfile>(isSuccessful: true, data);
            }

            return new Response<BaseProfile>(isSuccessful: false, null);
        }

        public async Task<Response<AuthorizationData>> AuthorizeAsync(string login, string pass)
        {
            var response = await profileService.AuthorizeAsync(login, pass, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<AuthorizationData>(isSuccessful: true, data);
            }

            return new Response<AuthorizationData>(isSuccessful: false, null);
        }

        public async Task<Response<ConfirmationResult>> RegistrationAsync(BaseProfile profile)
        {
            var response = await profileService.RegistrationAsync(profile.Map(), CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<ConfirmationResult>(isSuccessful: true, data);
            }

            return new Response<ConfirmationResult>(isSuccessful: false, null);
        }

        public async Task<Response<Data.Models.Profile.Profile>> GetProfileAsync()
        {
            var response = await profileService.GetProfileAsync(CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<Data.Models.Profile.Profile>(isSuccessful: true, data);
            }

            return new Response<Data.Models.Profile.Profile>(isSuccessful: false, null);
        }

        public async Task<Response<ProfileDiscount>> GetDiscountAsync()
        {
            var response = await profileService.GetDiscountAsync(CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<ProfileDiscount>(isSuccessful: true, data);
            }

            return new Response<ProfileDiscount>(isSuccessful: false, null);
        }

        public async Task<Response<Data.Models.Profile.Profile>> SavePersonalDataAsync(BaseProfile profile)
        {
            var response = await profileService.SavePersonalDataAsync(profile.Map(), CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<Data.Models.Profile.Profile>(isSuccessful: true, data);
            }

            return new Response<Data.Models.Profile.Profile>(isSuccessful: false, null);
        }
    }
}