using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models;
using SushiShop.Core.Services.Http.CommonInfo;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.CommonInfo
{
    public class CommonInfoManager : ICommonInfoManager
    {
        private readonly ICommonInfoService commonInfoService;

        public CommonInfoManager(ICommonInfoService commonInfoService)
        {
            this.commonInfoService = commonInfoService;
        }

        public async Task<Response<Data.Models.Job>> GetVacanciesAsync(string? city)
        {
            var response = await commonInfoService.GetVacanciesAsync(city, CancellationToken.None);
            if (response.IsSuccessful)
            {
                //var data = response.Data!.SuccessData!.Map();
                //return new Response<Job>(isSuccessful: true, data);

                return new Response<Job>(isSuccessful: true, new Job(string.Empty, string.Empty, string.Empty));
            }
            else
            {
                return new Response<Data.Models.Job>(
                    isSuccessful: false,
                    new Data.Models.Job(string.Empty, string.Empty, string.Empty));
            }
        }
    }
}