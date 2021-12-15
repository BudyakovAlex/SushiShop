using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Data.Models.Franchise;
using SushiShop.Core.Data.Models.Vacancy;
using SushiShop.Core.Mappers;
using SushiShop.Core.Services.Http.CommonInfo;
using System;
using System.Linq;
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

        public async Task<Response<Content>> GetContentAsync(string alias, long id, string? city)
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
                    DateTime.MinValue,
                    DateTime.MinValue));
        }

        public async Task<Response<Content>> GetBonusesContentAsync()
        {
            var response = await commonInfoService.GetBonusesContentAsync(CancellationToken.None);
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
                    DateTime.Now,
                    DateTime.Now));
        }

        public async Task<Response<CommonMenu[]>> GetCommonInfoMenuAsync()
        {
            var response = await commonInfoService.GetCommonInfoMenuAsync(CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData!.Select(menu => menu.Map()).ToArray();
                return new Response<CommonMenu[]>(isSuccessful: true, data, response.Data?.Errors ?? Array.Empty<string>());
            }

            return new Response<CommonMenu[]>(isSuccessful: false, Array.Empty<CommonMenu>(), response.Data?.Errors ?? Array.Empty<string>());
        }

        public async Task<Response<LinkedImage[]>> GetSocialNetworksAsync()
        {
            var response = await commonInfoService.GetSocialNetworksAsync(CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData!.Select(network => network.Map()).ToArray();
                return new Response<LinkedImage[]>(isSuccessful: true, data, response.Data?.Errors ?? Array.Empty<string>());
            }

            return new Response<LinkedImage[]>(isSuccessful: false, Array.Empty<LinkedImage>(), response.Data?.Errors ?? Array.Empty<string>());
        }

        public async Task<Response<LinkedImage[]>> GetBonusesImagesAsync()
        {
            var response = await commonInfoService.GetBonusesImagesAsync(CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData!.Select(network => network.Map()).ToArray();
                return new Response<LinkedImage[]>(isSuccessful: true, data, response.Data?.Errors ?? Array.Empty<string>());
            }

            return new Response<LinkedImage[]>(isSuccessful: false, Array.Empty<LinkedImage>(), response.Data?.Errors ?? Array.Empty<string>());
        }

        public async Task<Response<ApplicationInformation>> GetApplicationInformationAsync()
        {
            var response = await commonInfoService.GetApplicationInformationAsync(CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData!.Map();
                return new Response<ApplicationInformation>(isSuccessful: true, data);
            }

            return new Response<ApplicationInformation>(
                isSuccessful: false,
                new ApplicationInformation(
                    false,
                    string.Empty,
                    new Platforms(
                        new PlatformInformation(string.Empty, -1, string.Empty),
                        new PlatformInformation(string.Empty, -1, string.Empty))));
        }
    }
}