namespace SushiShop.Core.NavigationParameters.Abstract
{
    public abstract class SelectablePageNavigationParameters<TKey>
    {
        protected SelectablePageNavigationParameters(bool isSearchAvailable, bool isSingleSelection, params TKey[] selectedItemsIds)
        {
            SelectedItemsIds = selectedItemsIds;
            IsSearchAvailable = isSearchAvailable;
            IsSingleSelection = isSingleSelection;
        }

        public TKey[] SelectedItemsIds { get; }

        public bool IsSearchAvailable { get; }

        public bool IsSingleSelection { get; }
    }
}