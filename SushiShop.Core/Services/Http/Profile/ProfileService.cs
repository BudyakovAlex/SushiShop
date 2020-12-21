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
            var body = new { Login = login, SendCode = sendCode };
            return httpService.ExecuteAsync<ResponseDto<ProfileDto>>(
                Method.Post,
                Constants.Rest.ProfileCheckLogin,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<AuthorizationDataDto>>> AuthorizeAsync(string login, string pass, CancellationToken cancellationToken)
        {
            var body = new { Login = login, Pass = pass };
            return httpService.ExecuteAsync<ResponseDto<AuthorizationDataDto>>(
                Method.Post,
                Constants.Rest.ProfileAuth,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<ProfileRegistrationDto>>> RegistrationAsync(ProfileDto profileDto, CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<ProfileRegistrationDto>>(
                Method.Post,
                Constants.Rest.ProfileRegistration,
                profileDto,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<PersonalDataDto>>> GetPersonalDataAsync(CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<PersonalDataDto>>(
                Method.Post,
                Constants.Rest.ProfileGetPersonalData,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<ProfileDiscountDto>>> GetDiscountAsync(CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<ProfileDiscountDto>>(
                Method.Post,
                Constants.Rest.ProfileGetDiscount,
                cancellationToken);
        }
    }
}
