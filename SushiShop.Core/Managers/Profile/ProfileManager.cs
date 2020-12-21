using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Profile;
using SushiShop.Core.Mappers.Profile;
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

        public async Task<Response<LoginProfile>> CheckIsLoginAvailableAsync(string login, bool? sendCode)
        {
            var response = await profileService.CheckIsLoginAvailableAsync(login, sendCode, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<LoginProfile>(isSuccessful: true, data);
            }

            return new Response<LoginProfile>(isSuccessful: false, null);
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

        public async Task<Response<RegistrationProfile>> RegistrationAsync(RegistrationDataDto registrationDataDto)
        {
            var response = await profileService.RegistrationAsync(registrationDataDto, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<RegistrationProfile>(isSuccessful: true, data);
            }

            return new Response<RegistrationProfile>(isSuccessful: false, null);
        }

        public async Task<Response<PersonalData>> GetPersonalDataAsync()
        {
            var response = await profileService.GetPersonalDataAsync(CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<PersonalData>(isSuccessful: true, data);
            }

            return new Response<PersonalData>(isSuccessful: false, null);
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
    }
}