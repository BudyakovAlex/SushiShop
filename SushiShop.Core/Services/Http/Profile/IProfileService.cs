using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Http;

namespace SushiShop.Core.Services.Http.Profile
{
    public interface IProfileService
    {
        Task<HttpResponse<ResponseDto<BaseProfileDto>>> CheckIsLoginAvailableAsync(string? login, bool? shouldSendCode, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<AuthorizationDataDto>>> AuthorizeAsync(string login, string pass, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<ConfirmationResultDto>>> RegistrationAsync(BaseProfileDto profileDto, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<ProfileDto>>> GetProfileAsync(CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<ProfileDiscountDto>>> GetDiscountAsync(CancellationToken cancellationToken);
        
        Task<HttpResponse<ResponseDto<ProfileDto>>> SaveProfileAsync(BaseProfileDto profileDto, CancellationToken cancellationToken);
    }
}