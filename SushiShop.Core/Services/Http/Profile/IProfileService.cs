using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Http;

namespace SushiShop.Core.Services.Http.Profile
{
    public interface IProfileService
    {
        Task<HttpResponse<ResponseDto<CheckLoginDto[]>>> CheckLoginAsync(string login, bool? sendCode, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<AuthDto[]>>> AuthAsync(string login, string pass, CancellationToken cancellationToken);
                
        Task<HttpResponse<ResponseDto<RegistrationDto>>> RegistrationAsync(RegistrationDataDto registrationData, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<PersonalDataDto[]>>> GetPersonalDataAsync(CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<DiscountDto[]>>> GetDiscountAsync(CancellationToken cancellationToken);      
    }
}