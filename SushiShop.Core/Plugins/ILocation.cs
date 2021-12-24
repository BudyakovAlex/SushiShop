using System.Threading.Tasks;

namespace SushiShop.Core.Plugins
{
    public interface ILocation
    {
        Task RequestEnableLocationServiceAsync();
    }
}
