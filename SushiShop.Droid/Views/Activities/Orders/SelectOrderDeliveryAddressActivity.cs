using Android.App;
using SushiShop.Core.ViewModels.Orders;
using SushiShop.Droid.Views.Activities.Abstract;

namespace SushiShop.Droid.Views.Activities.Orders
{
    [Activity]
    public class SelectOrderDeliveryAddressActivity : BaseActivity<SelectOrderDeliveryAddressViewModel>
    {
        public SelectOrderDeliveryAddressActivity() : base(Resource.Layout.activity_common_info)
        {
        }
    }
}
