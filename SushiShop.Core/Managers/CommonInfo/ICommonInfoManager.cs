using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Franchise;
using SushiShop.Core.Data.Models.Vacancy;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.CommonInfo
{
    public interface ICommonInfoManager
    {
        Task<Response<Vacancy>> GetVacanciesAsync(string? city);

        Task<Response<Franchise>> GetFranchiseAsync();
    }
}