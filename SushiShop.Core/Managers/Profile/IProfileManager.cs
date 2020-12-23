using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Profile;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.Profile
{
    public interface IProfileManager
    {
        Task<Response<Data.Models.Profile.Profile?>> CheckIsLoginAvailableAsync(string? login, bool? shouldSendCode);

        Task<Response<AuthorizationData?>> AuthorizeAsync(string login, string pass);

        Task<Response<ConfirmationResult?>> RegistrationAsync(Data.Models.Profile.Profile profile);

        Task<Response<DetailedProfile?>> GetProfileAsync();

        Task<Response<ProfileDiscount?>> GetDiscountAsync();

        Task<Response<DetailedProfile?>> SaveProfileAsync(Data.Models.Profile.Profile profile);
    }
}