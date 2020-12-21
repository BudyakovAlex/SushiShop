using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Dtos.Menu;
using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Http;


namespace SushiShop.Core.Services.Http.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IHttpService httpService;

        public ProfileService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public Task<HttpResponse<ResponseDto<CheckLoginDto[]>>> CheckLoginAsync(string login, bool? sendCode, CancellationToken cancellationToken)
        {
            var body = new { Login = login, SendCode = sendCode };
            return httpService.ExecuteAsync<ResponseDto<CheckLoginDto[]>>(
                Method.Post,
                Constants.Rest.ProfileCheckLogin,
                body,
                cancellationToken);
        }


        public Task<HttpResponse<ResponseDto<MenuDto>>> GetMenuAsync(string? city, CancellationToken cancellationToken)
        {
            var body = new { City = city };
            return httpService.ExecuteAsync<ResponseDto<MenuDto>>(
                Method.Post,
                Constants.Rest.ProfileRegistration,
                body,
                cancellationToken);
        }
    }
}
