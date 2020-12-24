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
            string orderNumber,
            string question,
            string[] imagePaths,
            CancellationToken cancellationToken)
        {
            var body = new
            {
                question,
                orderId = orderNumber
            };

            return httpService.ExecuteMultipartAsync<ResponseDto<string>>(
                Method.Post,
                Constants.Rest.ProfileFeedbackResource,
                body,
                imagePaths,
                cancellationToken);
        }
    }
}