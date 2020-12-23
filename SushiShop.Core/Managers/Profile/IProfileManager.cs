using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Profile;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.Profile
{
    public interface IProfileManager
    {
        Task<Response<BaseProfile?>> CheckIsLoginAvailableAsync(string? login, bool? shouldSendCode);

        Task<Response<AuthorizationData?>> AuthorizeAsync(string login, string pass);

        Task<Response<ConfirmationResult?>> RegistrationAsync(BaseProfile profile);

        Task<Response<Data.Models.Profile.Profile?>> GetProfileAsync();

        Task<Response<ProfileDiscount?>> GetDiscountAsync();

        Task<Response<Data.Models.Profile.Profile?>> SaveProfileAsync(BaseProfile profile);
    }
}