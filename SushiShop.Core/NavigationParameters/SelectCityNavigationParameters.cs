using SushiShop.Core.NavigationParameters.Abstract;

namespace SushiShop.Core.NavigationParameters
{
    public class SelectCityNavigationParameters : SelectablePageNavigationParameters<long>
    {
        public SelectCityNavigationParameters(params long[] selectedItemsIds)
            : base(true, true, selectedItemsIds)
        {
        }
    }
}