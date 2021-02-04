using System;
using Foundation;
using SushiShop.Ios.Views.Controls;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Orders
{
    public class SelectOrderDeliverySearchController : UISearchController, IUISearchControllerDelegate
    {
        private const string CancelButtonTextKey = "cancelButtonText";

        private NSLayoutConstraint topTableViewConstraint;

        public SelfSizeTableView TableView { get; private set; }

        private string cancelButtonText;
        public string CancelButtonText
        {
            get => cancelButtonText;
            set
            {
                cancelButtonText = value;
                SearchBar.SetValueForKey(new NSString(cancelButtonText), new NSString(CancelButtonTextKey));
            }
        }

        public SelectOrderDeliverySearchController()
        {
            Initialize();
        }

        public SelectOrderDeliverySearchController(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public SelectOrderDeliverySearchController(UIViewController searchResultsController) : base(searchResultsController)
        {
            Initialize();
        }

        public SelectOrderDeliverySearchController(string nibName, NSBundle bundle) : base(nibName, bundle)
        {
            Initialize();
        }

        protected SelectOrderDeliverySearchController(NSObjectFlag t) : base(t)
        {
            Initialize();
        }

        protected internal SelectOrderDeliverySearchController(IntPtr handle) : base(handle)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            Initialize();
        }

        private void Initialize()
        {
            TableView = new SelfSizeTableView()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                ScrollEnabled = false,
                Hidden = true
            };

            this.Delegate = this;
            View.AddSubview(TableView);

            topTableViewConstraint = TableView.TopAnchor.ConstraintEqualTo(View.TopAnchor);
            NSLayoutConstraint.ActivateConstraints(new[]
            {
                topTableViewConstraint,
                TableView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor),
                TableView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor),
                TableView.BottomAnchor.ConstraintLessThanOrEqualTo(View.BottomAnchor)
            });
        }

        [Export("willPresentSearchController:")]
        public void WillPresentSearchController(UISearchController searchController)
        {
            topTableViewConstraint.Constant = SearchBar.Frame.Bottom;
            TableView.Hidden = false;
        }

        [Export("willDismissSearchController:")]
        public void WillDismissSearchController(UISearchController searchController)
        {
            TableView.Hidden = true;
        }
    }
}
