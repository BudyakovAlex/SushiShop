using SushiShop.Core.Data.Http;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.Feedback
{
    public interface IFeedbackManager
    {
        Task<Response<string?>> SendFeedbackAsync(string? orderNumber, string? question, string[] imagePaths);
    }
}