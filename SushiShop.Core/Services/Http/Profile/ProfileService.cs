using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Http;

namespace SushiShop.Core.Services.Http.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IHttpService httpService;

        public ProfileService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public Task<HttpResponse<ResponseDto<ProfileDto>>> CheckIsLoginAvailableAsync(string login, bool? sendCode, CancellationToken cancellationToken)
        {
            var body = new { login, sendCode };
            return httpService.ExecuteAsync<ResponseDto<ProfileDto>>(
                Method.Post,
                Constants.Rest.ProfileCheckLoginResource,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<AuthorizationDataDto>>> AuthorizeAsync(string login, string pass, CancellationToken cancellationToken)
        {
            var body = new { login, pass };
            return httpService.ExecuteAsync<ResponseDto<AuthorizationDataDto>>(
                Method.Post,
                Constants.Rest.ProfileAuthResource,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<ProfileRegistrationDto>>> RegistrationAsync(ProfileDto profileDto, CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<ProfileRegistrationDto>>(
                Method.Post,
                Constants.Rest.ProfileRegistrationResource,
                profileDto,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<PersonalDataDto>>> GetPersonalDataAsync(CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<PersonalDataDto>>(
                Method.Post,
                Constants.Rest.ProfileGetPersonalDataResource,
                null,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<ProfileDiscountDto>>> GetDiscountAsync(CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<ProfileDiscountDto>>(
                Method.Post,
                Constants.Rest.ProfileGetDiscountResource,
                null,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<PersonalDataDto>>> SavePersonalDataAsync(ProfileDto profileDto, CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<PersonalDataDto>>(
                Method.Post,
                Constants.Rest.ProfileSaveResource,
                profileDto,
                cancellationToken);
        }
    }
}
