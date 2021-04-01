using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Services.Http.Feedback;

namespace SushiShop.Core.Managers.Feedback
{
    public class FeedbackManager : IFeedbackManager
    {
        private readonly IFeedbackService feedbackService;

        public FeedbackManager(IFeedbackService feedbackService)
        {
            this.feedbackService = feedbackService;
        }

        public async Task<Response<string?>> SendFeedbackAsync(
            string? orderNumber,
            string? question,
            string[] imagePaths)
        {
            var response = await feedbackService.SendFeedbackAsync(
                orderNumber,
                question,
                imagePaths,
                CancellationToken.None);
            if (response.IsSuccessful)
            {
                return new Response<string?>(isSuccessful: true, response.Data!.SuccessData);
            }

            return new Response<string?>(isSuccessful: false, null, response.Data!.Errors!);
        }
    }
}