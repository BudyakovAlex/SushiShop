using SushiShop.Core.Data.Dtos.Job;
using SushiShop.Core.Data.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Services.Http.CommonInfo
{
    public interface ICommonInfoService
    {
        Task<HttpResponse<ResponseDto<JobDto>>> GetVacanciesAsync(string? city, CancellationToken cancellationToken);
    }
}