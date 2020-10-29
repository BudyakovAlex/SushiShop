using System;
using System.ComponentModel;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;

namespace SushiShop.Ios.Views.Controls.Abstract
{
    public abstract class BaseView<TViewModel> : MvxView
        where TViewModel : INotifyPropertyChanged
    {
        private TViewModel _viewModel;

        protected BaseView()
        {
            InitializeAndBind();
        }

        protected BaseView(CGRect frame)
            : base(frame)
        {
            InitializeAndBind();
        }

        protected BaseView(IntPtr handle)
            : base(handle)
        {
        }

        public TViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                if (value == null)
                {
                    return;
                }

                _viewModel = value;
                DataContext = _viewModel;
            }
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            InitializeAndBind();
        }

        protected virtual void Initialize()
        {
        }

        protected virtual void Bind()
        {
        }

        private void InitializeAndBind()
        {
            Initialize();
            this.DelayBind(Bind);
        }
    }
}
