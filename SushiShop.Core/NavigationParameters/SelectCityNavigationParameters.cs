using SushiShop.Core.NavigationParameters.Abstract;
using System.Collections.Generic;

namespace SushiShop.Core.NavigationParameters
{
    public class SelectCityNavigationParameters : SelectablePageNavigationParameters<int>
    {
        public SelectCityNavigationParameters(List<int> selectedItemsIds)
            : base(selectedItemsIds, true, true)
        {
        }
    }
}