using SushiShop.Core.Common;
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

        public async Task<HttpResponse<ResponseDto<JobDto>>> GetVacanciesAsync(string? city, CancellationToken cancellationToken)
        {
            return await httpService.ExecuteAsync<ResponseDto<JobDto>>(
                Method.Post,
                Constants.Rest.JobResource,
                cancellationToken);
        }
    }
}