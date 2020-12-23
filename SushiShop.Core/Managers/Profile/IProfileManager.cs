using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Profile;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.Profile
{
    public interface IProfileManager
    {
        Task<Response<Data.Models.Profile.Profile>> CheckIsLoginAvailableAsync(string login, bool? sendCode);

        Task<Response<AuthorizationData>> AuthorizeAsync(string login, string pass);

        Task<Response<ProfileRegistration>> RegistrationAsync(Data.Models.Profile.Profile profile);

        Task<Response<PersonalData>> GetPersonalDataAsync();

        Task<Response<ProfileDiscount>> GetDiscountAsync();

        Task<Response<PersonalData>> SavePersonalDataAsync(Data.Models.Profile.Profile profile);
    }
}