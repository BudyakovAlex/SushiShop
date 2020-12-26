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

        public Task<HttpResponse<ResponseDto<LoginValidationResultDto>>> CheckIsLoginAvailableAsync(string? login, CancellationToken cancellationToken)
        {
            var body = new { login };
            return httpService.ExecuteAnonymouslyAsync<ResponseDto<LoginValidationResultDto>>(
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

        public Task<HttpResponse<ResponseDto<ConfirmationResultDto>>> RegistrationAsync(ProfileDto profileDto, CancellationToken cancellationToken)
        {
            return httpService.ExecuteAnonymouslyAsync<ResponseDto<ConfirmationResultDto>>(
                Method.Post,
                Constants.Rest.ProfileRegistrationResource,
                profileDto,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<DetailedProfileDto>>> GetProfileAsync(CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<DetailedProfileDto>>(
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

        public Task<HttpResponse<ResponseDto<DetailedProfileDto>>> SaveProfileAsync(ProfileDto profileDto, CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<DetailedProfileDto>>(
                Method.Post,
                Constants.Rest.ProfileSaveResource,
                profileDto,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<string>>> UploadPhotoAsync(string imagePath, CancellationToken cancellationToken)
        {
            return httpService.ExecuteMultipartAsync<ResponseDto<string>>(
                Method.Post,
                Constants.Rest.ProfileUploadPhotoResource,
                null,
                new string[] { imagePath },
                cancellationToken);
        }
    }
}
