using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace SushiShop.Ios.Views.Controls.Abstract
{
    public abstract class BaseView : UIView, INotifyPropertyChanged
    {
        protected BaseView()
        {
            Initialize();
        }

        protected BaseView(CGRect frame)
            : base(frame)
        {
            Initialize();
        }

        protected BaseView(IntPtr handle)
            : base(handle)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            Initialize();
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void Initialize()
        {
        }
    }

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
