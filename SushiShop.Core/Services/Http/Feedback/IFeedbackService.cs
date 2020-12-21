using SushiShop.Core.Data.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Services.Http.Feedback
{
    public interface IFeedbackService
    {
        Task<HttpResponse<ResponseDto<string>>> SendFeedbackAsync(string? orderNumber, string? question, string[]? files, CancellationToken cancellationToken);
    }
}