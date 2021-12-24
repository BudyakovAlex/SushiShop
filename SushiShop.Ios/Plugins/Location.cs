using System.Threading.Tasks;
using SushiShop.Core.Plugins;

namespace SushiShop.Ios.Plugins
{
    public class Location : ILocation
    {
        public Task RequestEnableLocationServiceAsync()
        {
            return Task.CompletedTask;
        }
    }
}
