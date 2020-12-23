using SushiShop.Core.Data.Models.Profile;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Mappers.Profile;
using SushiShop.Core.Services.Http.Profile;
using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Dtos.Profile;

namespace SushiShop.Core.Managers.Profile
{
    public class ProfileManager : IProfileManager
    {
        private readonly IProfileService profileService;

        public ProfileManager(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        public async Task<Response<Data.Models.Profile.Profile>> CheckIsLoginAvailableAsync(string login, bool? sendCode)
        {
            var response = await profileService.CheckIsLoginAvailableAsync(login, sendCode, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<Data.Models.Profile.Profile>(isSuccessful: true, data);
            }

            return new Response<Data.Models.Profile.Profile>(isSuccessful: false, null);
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

        public async Task<Response<ProfileRegistration>> RegistrationAsync(Data.Models.Profile.Profile profile)
        {
            var profileDto = new ProfileDto
            {
                Email = profile.Email,
                Phone = profile.Phone,
                DateOfBirth = profile.DateOfBirth,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                FullName = profile.FullName,
                Gender = profile.Gender,
                IsAllowSubscribe = profile.IsAllowSubscribe,
                IsAllowNotifications = profile.IsAllowNotifications,
                IsAllowPush = profile.IsAllowPush,
                IsNeedRegistration = profile.IsNeedRegistration
            };

            var response = await profileService.RegistrationAsync(profileDto, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<ProfileRegistration>(isSuccessful: true, data);
            }

            return new Response<ProfileRegistration>(isSuccessful: false, null);
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