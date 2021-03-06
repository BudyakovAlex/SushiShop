using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Simple;
using MvvmCross.Commands;
using SushiShop.Core.Extensions;
using SushiShop.Core.NavigationParameters.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Abstract
{
    public abstract class SelectablePageItemsViewModel<TSelectableItemViewModel, TNavigationParameters, TKey>
        : BaseItemsPageViewModel<TSelectableItemViewModel, TNavigationParameters, List<TSelectableItemViewModel>?>
        where TSelectableItemViewModel : SelectableItemViewModel<TKey>
        where TNavigationParameters : SelectablePageNavigationParameters<TKey>
    {
        private readonly List<TKey> selectedItemsIds = new List<TKey>();

        public SelectablePageItemsViewModel()
        {
            OriginalSource = new List<TSelectableItemViewModel>();

            CloseWithSelectedDataCommand = new SafeAsyncCommand(ExecutionStateWrapper, CloseWithSelectedDataAsync);
            SelectItemCommand = new SafeAsyncCommand<TSelectableItemViewModel>(ExecutionStateWrapper, OnItemSelectedAsync);
            ShowResultsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowResultsAsync);
        }

        public virtual string QueryPlaceholder { get; } = string.Empty;

        public IMvxCommand ShowResultsCommand { get; }

        public IMvxCommand CloseWithSelectedDataCommand { get; }

        public IMvxCommand<TSelectableItemViewModel> SelectItemCommand { get; }

        public bool IsSingleSelection { get; protected set; }

        public bool IsSearchAvailable { get; protected set; }

        private string query = string.Empty;

        public string Query
        {
            get => query;
            set
            {
                query = value;
                ShowResultsCommand.Execute();
            }
        }

        protected List<TSelectableItemViewModel> OriginalSource { get; }

        public override void Prepare(TNavigationParameters parameter)
        {
            selectedItemsIds.AddRange(parameter.SelectedItemsIds);
            IsSingleSelection = parameter.IsSingleSelection;
            IsSearchAvailable = parameter.IsSearchAvailable;
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            var itemsViewModels = await LoadDataAsync(selectedItemsIds);
            OriginalSource.AddRange(itemsViewModels);
            Items.AddRange(OriginalSource);
        }

        protected abstract Task<List<TSelectableItemViewModel>> LoadDataAsync(List<TKey> selectedItemId);

        protected virtual async Task OnItemSelectedAsync(TSelectableItemViewModel item)
        {
            if (!IsSingleSelection)
            {
                item.IsSelected = !item.IsSelected;
                return;
            }

            if (item.IsSelected)
            {
                return;
            }

            foreach (var itemViewModel in Items)
            {
                itemViewModel.IsSelected = itemViewModel == item;
            }

            await CloseWithSelectedDataAsync();
        }

        protected virtual Task ShowResultsAsync()
        {
            if (string.IsNullOrEmpty(Query))
            {
                Items.ReplaceWith(OriginalSource);
                return Task.CompletedTask;
            }

            var itemsWithQueryOnStart = OriginalSource.Where(item => item.Text.ToLower().StartsWith(Query.ToLower())).ToList();
            var itemsWithQueryInMiddle = OriginalSource.Where(item => item.Text.ToLower().Contains(Query.ToLower())).ToList();

            var mergedItems = itemsWithQueryOnStart.Union(itemsWithQueryInMiddle)
                                                   .DistinctBy(item => item.Text)
                                                   .ToList();
            Items.ReplaceWith(mergedItems);
            return Task.CompletedTask;
        }

        private Task CloseWithSelectedDataAsync()
        {
            var selectedItems = Items.Where(vm => vm.IsSelected).ToList();
            return NavigationManager.CloseAsync(this, selectedItems);
        }
    }
}