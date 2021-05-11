using System;
using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Http;

namespace SushiShop.Core.Services.Http.Profile
{
    public interface IProfileService
    {
        Task<HttpResponse<ResponseDto<LoginValidationResultDto>>> CheckIsLoginAvailableAsync(string? login, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<AuthorizationDataDto>>> AuthorizeAsync(string login, string pass, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<ConfirmationResultDto>>> RegistrationAsync(ProfileDto profileDto, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<DetailedProfileDto>>> GetProfileAsync(CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<ProfileDiscountDto>>> GetDiscountAsync(CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<DetailedProfileDto>>> SaveProfileAsync(ProfileDto profileDto, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<string>>> UploadPhotoAsync(string imagePath, CancellationToken cancellationToken);
    }
}