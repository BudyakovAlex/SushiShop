using System.Threading.Tasks;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Profile;

namespace SushiShop.Core.Managers.Profile
{
    public interface IProfileManager
    {
        Task<Response<LoginValidationResult?>> CheckIsLoginAvailableAsync(string? login);

        Task<Response<AuthorizationData?>> AuthorizeAsync(string login, string pass);

        Task<Response<ConfirmationResult?>> RegistrationAsync(Data.Models.Profile.Profile profile);

        Task<Response<DetailedProfile?>> GetProfileAsync();

        Task<Response<ProfileDiscount?>> GetDiscountAsync();

        Task<Response<DetailedProfile?>> SaveProfileAsync(Data.Models.Profile.Profile profile);

        Task<Response<string?>> UploadPhotoAsync(string imagePath);
    }
}