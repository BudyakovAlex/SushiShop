using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Orders;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Orders
{
    [MvxChildPresentation]
    public partial class OrderCompositionViewController : BaseViewController<OrderCompositionViewModel>
    {
    }
}
