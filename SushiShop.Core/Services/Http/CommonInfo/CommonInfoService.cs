using SushiShop.Core.Common;
using SushiShop.Core.Data.Dtos.Common;
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

        public Task<HttpResponse<ResponseDto<ContentDto>>> GetContentAsync(string alias, long id, string? city, CancellationToken cancellationToken)
        {
            var body = new
            {
                alias,
                id,
                city
            };

            return httpService.ExecuteAsync<ResponseDto<ContentDto>>(
                Method.Post,
                Constants.Rest.GetResource,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<CommonMenuDto[]>>> GetCommonInfoMenuAsync(CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<CommonMenuDto[]>>(
                Method.Post,
                Constants.Rest.GetMenuList,
                null,
                cancellationToken);
        }
    }
}