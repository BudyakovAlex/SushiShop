using System.Collections.Generic;

namespace SushiShop.Core.NavigationParameters.Abstract
{
    public abstract class SelectablePageNavigationParameters<TKey>
    {
        protected SelectablePageNavigationParameters(List<TKey> selectedItemsIds, bool isSearchAvailable, bool isSingleSelection)
        {
            SelectedItemsIds = selectedItemsIds;
            IsSearchAvailable = isSearchAvailable;
            IsSingleSelection = isSingleSelection;
        }

        public List<TKey> SelectedItemsIds { get; }

        public bool IsSearchAvailable { get; }

        public bool IsSingleSelection { get; }
    }
}