using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using Google.Android.Material.TextField;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Feedback;
using SushiShop.Droid.Presenter.Attributes;

namespace SushiShop.Droid.Views.Fragments.Feedback
{
    [NestedFragmentPresentation(FragmentContentId = Resource.Id.container_view)]
    public class FeedbackFragment : BaseFragment<FeedbackViewModel>
    {
        private AndroidX.AppCompat.Widget.Toolbar toolbar;
        private View loadingOverlayView;
        private MvxRecyclerView recyclerView;
        private TextInputLayout orderNumberTextInputLayout;
        private TextInputLayout questionTextInputLayout;
        private EditText orderNumberEditText;
        private EditText questionEditText;
        private View pickPhotoContainerView;
        private Button confirmButton;

        public FeedbackFragment() : base(Resource.Layout.fragment_feedback)
        {
        }

        protected override void InitializeViewPoroperties(View view, Bundle savedInstanceState)
        {
            base.InitializeViewPoroperties(view, savedInstanceState);

            toolbar = view.FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            loadingOverlayView = view.FindViewById<View>(Resource.Id.loading_overlay_view);
            recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recycler_view);
            orderNumberTextInputLayout = view.FindViewById<TextInputLayout>(Resource.Id.order_number_text_input_layout);
            questionTextInputLayout = view.FindViewById<TextInputLayout>(Resource.Id.question_text_input_layout);

            orderNumberEditText = view.FindViewById<EditText>(Resource.Id.order_number_edit_text);
            questionEditText = view.FindViewById<EditText>(Resource.Id.question_edit_text);
            pickPhotoContainerView = view.FindViewById<View>(Resource.Id.pick_photo_container_view);

            confirmButton = view.FindViewById<Button>(Resource.Id.confirm_button);
            confirmButton.SetRoundedCorners(Context.DpToPx(25));

            InitializeRecyclerView(view);
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(loadingOverlayView).For(v => v.BindVisible()).To(v => v.IsBusy);
            bindingSet.Bind(toolbar).For(v => v.Title).To(v => v.Title);
            bindingSet.Bind(recyclerView).For(v => v.ItemsSource).To(vm => vm.Photos);

            bindingSet.Bind(questionEditText).For(v => v.Text).To(vm => vm.Question);
            bindingSet.Bind(orderNumberEditText).For(v => v.Text).To(vm => vm.OrderNumber);
            bindingSet.Bind(questionTextInputLayout).For(v => v.Hint).To(vm => vm.QuestionPlaceholder);
            bindingSet.Bind(orderNumberTextInputLayout).For(v => v.Hint).To(vm => vm.OrderNumberPlaceholder);
            bindingSet.Bind(pickPhotoContainerView).For(v => v.BindClick()).To(vm => vm.UploadPhotosCommand);
            bindingSet.Bind(confirmButton).For(v => v.BindClick()).To(vm => vm.SendFeedbackCommand);
        }

        private void InitializeRecyclerView(View view)
        {
            recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recycler_view);
            recyclerView.SetLayoutManager(new MvxGuardedLinearLayoutManager(Context) { Orientation = LinearLayoutManager.Horizontal });
            recyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            recyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<OrderItemViewModel, OrderItemViewHolder>(Resource.Layout.item_order);
        }
    }
}
