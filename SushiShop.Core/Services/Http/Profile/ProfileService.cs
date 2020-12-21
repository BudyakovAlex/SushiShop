using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Dtos.Menu;
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

        public Task<HttpResponse<ResponseDto<CheckLoginDto[]>>> CheckLoginAsync(string login, bool? sendCode, CancellationToken cancellationToken)
        {
            var body = new { Login = login, SendCode = sendCode };
            return httpService.ExecuteAsync<ResponseDto<CheckLoginDto[]>>(
                Method.Post,
                Constants.Rest.ProfileCheckLogin,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<AuthDto[]>>> AuthAsync(string login, string pass, CancellationToken cancellationToken)
        {
            var body = new { Login = login, Pass = pass };
            return httpService.ExecuteAsync<ResponseDto<AuthDto[]>>(
                Method.Post,
                Constants.Rest.ProfileAuth,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<RegistrationDto>>> RegistrationAsync(RegistrationDataDto registrationData, CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<RegistrationDto>>(
                Method.Post,
                Constants.Rest.ProfileRegistration,
                registrationData,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<PersonalDataDto[]>>> GetPersonalDataAsync(CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<PersonalDataDto[]>>(
                Method.Post,
                Constants.Rest.ProfileGetPersonalData,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<DiscountDto[]>>> GetDiscountAsync(CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<DiscountDto[]>>(
                Method.Post,
                Constants.Rest.ProfileGetDiscount,
                cancellationToken);
        }
    }
}
