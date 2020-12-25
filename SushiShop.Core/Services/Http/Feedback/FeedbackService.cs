using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Http;

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
            string orderId,
            string question,
            string[] imagePaths,
            CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string>
            {
                [nameof(question)] = question,
                [nameof(orderId)] = orderId
            };

            return httpService.ExecuteMultipartAsync<ResponseDto<string>>(
                Method.Post,
                Constants.Rest.ProfileFeedbackResource,
                parameters,
                imagePaths,
                cancellationToken);
        }
    }
}