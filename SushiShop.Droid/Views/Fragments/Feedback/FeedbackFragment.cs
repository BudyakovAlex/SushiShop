using Android.Views;
using AndroidX.AppCompat.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using MvvmCross.DroidX.RecyclerView;
using SushiShop.Core.ViewModels.Feedback;
using SushiShop.Droid.Presenter.Attributes;

namespace SushiShop.Droid.Views.Fragments.Feedback
{
    [NestedFragmentPresentation(FragmentContentId = Resource.Id.container_view)]
    public class FeedbackFragment : BaseFragment<FeedbackViewModel>
    {
        private Toolbar toolbar;
        private View loadingOverlayView;
        private MvxRecyclerView recyclerView;

        public FeedbackFragment() : base(Resource.Layout.fragment_feedback)
        {
        }
    }
}
