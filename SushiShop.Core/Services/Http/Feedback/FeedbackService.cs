using SushiShop.Core.Common;
using SushiShop.Core.Data.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Services.Http.Feedback
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IHttpService httpService;

        public FeedbackService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public Task<HttpResponse<ResponseDto<string>>> SendFeedbackAsync(
            string? orderNumber,
            string? question,
            string[]? files,
            CancellationToken cancellationToken)
        {
            files ??= Array.Empty<string>();
            var body = new
            {
                question,
                orderId = orderNumber
            };

            return httpService.ExecuteMultipartAsync<ResponseDto<string>>(
                Method.Post,
                Constants.Rest.ProfileFeedbackResource,
                body,
                files,
                cancellationToken);
        }
    }
}