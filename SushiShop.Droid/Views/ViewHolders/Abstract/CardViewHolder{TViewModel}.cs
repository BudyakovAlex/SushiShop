using Android.Views;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters.ViewHolders;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.ViewModels;

namespace SushiShop.Droid.Views.ViewHolders.Abstract
{
    public class CardViewHolder<TViewModel> : CardViewHolder
        where TViewModel : MvxNotifyPropertyChanged
    {
        public CardViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        public TViewModel ViewModel => BindingContext as TViewModel;

        public MvxFluentBindingDescriptionSet<CardViewHolder<TViewModel>, TViewModel> CreateBindingSet()
        {
            return this.CreateBindingSet<CardViewHolder<TViewModel>, TViewModel>();
        }
    }
}