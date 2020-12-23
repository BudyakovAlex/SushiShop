using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Franchise;
using SushiShop.Core.Data.Models.Vacancy;
using SushiShop.Core.Mappers.Common;
using SushiShop.Core.Mappers.Vacancy;
using SushiShop.Core.Mappers.Franchise;
using SushiShop.Core.Services.Http.CommonInfo;
using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Models.Common;
using System;

namespace SushiShop.Core.Managers.CommonInfo
{
    public class CommonInfoManager : ICommonInfoManager
    {
        private readonly ICommonInfoService commonInfoService;

        public CommonInfoManager(ICommonInfoService commonInfoService)
        {
            this.commonInfoService = commonInfoService;
        }

        public async Task<Response<Franchise>> GetFranchiseAsync()
        {
            var response = await commonInfoService.GetFranchiseAsync(CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData!.Map();
                return new Response<Franchise>(isSuccessful: true, data);
            }

            return new Response<Franchise>(
                isSuccessful: false,
                new Franchise(string.Empty, string.Empty));
        }

        public async Task<Response<Vacancy>> GetVacanciesAsync(string? city)
        {
            var response = await commonInfoService.GetVacanciesAsync(city, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData!.Map();
                return new Response<Vacancy>(isSuccessful: true, data);
            }

            return new Response<Vacancy>(
                isSuccessful: false,
                new Vacancy(string.Empty, string.Empty, string.Empty));
        }

        public async Task<Response<Content>> GetContentAsync(string alias, int id, string? city)
        {
            var response = await commonInfoService.GetContentAsync(alias, id, city, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData!.Map();
                return new Response<Content>(isSuccessful: true, data);
            }

            return new Response<Content>(
                isSuccessful: false,
                new Content(
                    int.MinValue,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    DateTime.MinValue,
                    DateTime.MinValue));
        }
    }
}