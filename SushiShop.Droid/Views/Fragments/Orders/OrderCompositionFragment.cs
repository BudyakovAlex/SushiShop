using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using SushiShop.Core.ViewModels.Orders;
using SushiShop.Droid.Presenter.Attributes;

namespace SushiShop.Droid.Views.Fragments.Orders
{
    [NestedFragmentPresentation(FragmentContentId = Resource.Id.container_view)]
    public class OrderCompositionFragment : BaseFragment<OrderCompositionViewModel>
    {
        public OrderCompositionFragment() : base(resourceId)
        {
        }
    }
}
