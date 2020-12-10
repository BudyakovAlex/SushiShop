using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.Common;
using SushiShop.Core.Managers.Orders;
using SushiShop.Core.ViewModels.Orders.Items;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Orders
{
    public class MyOrdersViewModel : BaseItemsPageViewModel<OrderItemViewModel>
    {
        private readonly IOrdersManager ordersManager;

        public MyOrdersViewModel(IOrdersManager ordersManager)
        {
            this.ordersManager = ordersManager;

            Pagination = new PaginationViewModel(LoadMoreItemsAsync, Constants.Common.DefaultPaginationSize);
        }

        public PaginationViewModel Pagination { get; }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            _ = RefreshDataAsync();
        }

        protected override Task RefreshDataAsync()
        {
            Pagination.Reset();
            return LoadMoreItemsAsync(0, Constants.Common.DefaultPaginationSize);
        }

        private async Task<int> LoadMoreItemsAsync(int paginationIndex, int paginationSize)
        {
            var response = await ordersManager.GetMyOrdersAsync(paginationIndex, paginationSize).ConfigureAwait(false);
            if (!response.IsSuccessful)
            {
                return 0;
            }

            Pagination.SetTotalItemsCount(response.Data.TotalCount);

            var viewModels = response.Data.Data.Select(item => new OrderItemViewModel(item)).ToList();
            Items.AddRange(viewModels);
            return response.Data.CurrentLimit;
        }
    }
}