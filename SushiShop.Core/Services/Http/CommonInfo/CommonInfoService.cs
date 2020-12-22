using SushiShop.Core.Common;
using SushiShop.Core.Data.Dtos.Franchise;
using SushiShop.Core.Data.Dtos.Job;
using SushiShop.Core.Data.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Services.Http.CommonInfo
{
    public class CommonInfoService : ICommonInfoService
    {
        private readonly IHttpService httpService;

        public CommonInfoService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public Task<HttpResponse<ResponseDto<FranchiseDto>>> GetFranchiseAsync(CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<FranchiseDto>>(
                Method.Post,
                Constants.Rest.FranchiseResource,
                null,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<VacancyDto>>> GetVacanciesAsync(string? city, CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<VacancyDto>>(
                Method.Post,
                Constants.Rest.JobResource,
                null,
                cancellationToken);
        }
    }
}