using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Http;

namespace SushiShop.Core.Services.Http.Profile
{
    public interface IProfileService
    {
        Task<HttpResponse<ResponseDto<ProfileDto>>> CheckIsLoginAvailableAsync(string login, bool? sendCode, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<AuthorizationDataDto>>> AuthorizeAsync(string login, string pass, CancellationToken cancellationToken);
                
        Task<HttpResponse<ResponseDto<ProfileRegistrationDto>>> RegistrationAsync(ProfileDto profileDto, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<PersonalDataDto>>> GetPersonalDataAsync(CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<ProfileDiscountDto>>> GetDiscountAsync(CancellationToken cancellationToken);      
    }
}