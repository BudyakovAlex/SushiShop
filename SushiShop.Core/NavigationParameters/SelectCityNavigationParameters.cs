using SushiShop.Core.NavigationParameters.Abstract;

namespace SushiShop.Core.NavigationParameters
{
    public class SelectCityNavigationParameters : SelectablePageNavigationParameters<int>
    {
        public SelectCityNavigationParameters(params int[] selectedItemsIds)
            : base(true, true, selectedItemsIds)
        {
        }
    }
}