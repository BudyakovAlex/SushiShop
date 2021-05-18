using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Common.Items;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Feedback
{
    public class FeedbackPhotoItemViewHolder : CardViewHolder<PhotoItemViewModel>
    {
        private View view;
        private ImageView photoImageView;
        private ImageView deleteImageView;

        public FeedbackPhotoItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            this.view = view;
            photoImageView = view.FindViewById<ImageView>(Resource.Id.photo_image_view);
            deleteImageView = view.FindViewById<ImageView>(Resource.Id.delete_image_view);
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(photoImageView).For(v => v.BindUrl()).To(vm => vm.ImagePath);
            bindingSet.Bind(deleteImageView).For(v => v.BindClick()).To(vm => vm.RemoveCommand);
            bindingSet.Bind(view).For(v => v.BindClick()).To(vm => vm.ShowDetailsCommand);
        }
    }
}
