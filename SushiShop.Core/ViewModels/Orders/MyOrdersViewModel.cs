using System.Linq;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.Common;
using SushiShop.Core.Managers.Orders;
using SushiShop.Core.Messages;
using SushiShop.Core.Providers;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Orders.Items;

namespace SushiShop.Core.ViewModels.Orders
{
    public class MyOrdersViewModel : BaseItemsPageViewModel<OrderItemViewModel>
    {
        private readonly IOrdersManager ordersManager;
        private readonly IUserSession userSession;

        public MyOrdersViewModel(IOrdersManager ordersManager, IUserSession userSession)
        {
            this.ordersManager = ordersManager;
            this.userSession = userSession;

            Pagination = new PaginationViewModel(LoadMoreItemsAsync, Constants.Common.DefaultPaginationSize);
        }

        public PaginationViewModel Pagination { get; }

        public string Title => AppStrings.MyOrders;

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set => SetProperty(ref isLoading, value);
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            await RefreshDataAsync();
        }

        protected override async Task RefreshDataAsync()
        {
            IsLoading = true;

            Pagination.Reset();
            await LoadMoreItemsAsync(0, Constants.Common.DefaultPaginationSize);

            IsLoading = false;
        }

        private async Task<int> LoadMoreItemsAsync(int paginationIndex, int paginationSize)
        {
            var response = await ordersManager.GetMyOrdersAsync(paginationIndex, paginationSize).ConfigureAwait(false);
            if (!response.IsSuccessful)
            {
                return 0;
            }

            Pagination.SetTotalItemsCount(response.Data.TotalCount);

            var viewModels = response.Data.Data.Select(item => new OrderItemViewModel(item, RepeatOrderAsync)).ToList();
            Items.AddRange(viewModels);
            return response.Data.CurrentLimit;
        }

        private async Task RepeatOrderAsync(long id)
        {
            var city = userSession.GetCity();
            await ordersManager.RepeatOrderAsync(id, city?.Name);
            Messenger.Publish(new RefreshCartMessage(this));

            await RefreshDataAsync();
        }
    }
}