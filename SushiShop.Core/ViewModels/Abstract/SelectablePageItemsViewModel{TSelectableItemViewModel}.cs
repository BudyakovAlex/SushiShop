using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Simple;
using MvvmCross.Commands;
using SushiShop.Core.NavigationParameters.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Abstract
{
    public abstract class SelectablePageItemsViewModel<TSelectableItemViewModel, TNavigationParameters, Tkey>
        : BaseItemsPageViewModel<TSelectableItemViewModel, TNavigationParameters, List<TSelectableItemViewModel>>
        where TSelectableItemViewModel : SelectableItemViewModel<Tkey>
        where TNavigationParameters : SelectablePageNavigationParameters<Tkey>
    {
        private List<Tkey> selectedItemsIds;

        public SelectablePageItemsViewModel()
        {
            selectedItemsIds = new List<Tkey>();
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
            selectedItemsIds.AddRange(parameter.SelectedItemsIds ?? new List<Tkey>());
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

        protected abstract Task<List<TSelectableItemViewModel>> LoadDataAsync(List<Tkey> selectedItemId);

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

            var searchedItems = OriginalSource.Where(item => item.Text.ToLower().Contains(Query.ToLower()));

            Items.ReplaceWith(searchedItems);
            return Task.CompletedTask;
        }

        private Task CloseWithSelectedDataAsync()
        {
            var selectedItems = Items.Where(vm => vm.IsSelected).ToList();
            return NavigationManager.CloseAsync(this, selectedItems);
        }
    }
}