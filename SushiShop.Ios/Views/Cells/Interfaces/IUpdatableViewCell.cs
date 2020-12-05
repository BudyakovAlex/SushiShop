using System;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Interfaces
{
    public interface IUpdatableViewCell
    {
        Action<UIView> RefreshLayoutAction { get; set; } 
    }
}
