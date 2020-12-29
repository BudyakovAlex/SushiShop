using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Dtos.Franchise;
using SushiShop.Core.Data.Dtos.Job;
using SushiShop.Core.Data.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Services.Http.CommonInfo
{
    public interface ICommonInfoService
    {
        Task<HttpResponse<ResponseDto<VacancyDto>>> GetVacanciesAsync(string? city, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<FranchiseDto>>> GetFranchiseAsync(CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<ContentDto>>> GetContentAsync(string alias, long id, string? city, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<CommonMenuDto[]>>> GetCommonInfoMenuAsync(CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<SocialNetworkDto[]>>> GetSocialNetworksAsync(CancellationToken cancellationToken);
    }
}