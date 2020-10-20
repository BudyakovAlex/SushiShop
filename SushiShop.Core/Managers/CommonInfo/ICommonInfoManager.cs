using SushiShop.Core.Data.Http;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.CommonInfo
{
    public interface ICommonInfoManager
    {
        Task<Response<Data.Models.Job>> GetVacanciesAsync(string? city);
    }
}