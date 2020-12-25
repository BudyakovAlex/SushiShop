#nullable enable

using System.Linq;
using System.Threading.Tasks;
using CoreFoundation;
using SushiShop.Core.Data.Models.Plugins;
using SushiShop.Core.Extensions;
using SushiShop.Core.Plugins;
using SushiShop.Ios.Extensions;
using UIKit;

namespace SushiShop.Ios.Plugins
{
    public class Dialog : IDialog
    {
        public Task ShowActionSheetAsync(string? title, string? message, string cancelTitle, params DialogAction[] actions)
        {
            var tcs = new TaskCompletionSource<object?>();

            DispatchQueue.MainQueue.DispatchAsync(() =>
            {
                var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.ActionSheet);

                actions.Select(CreateDefaultAlertAction).ForEach(alertController.AddAction);
                alertController.AddAction(UIAlertAction.Create(cancelTitle, UIAlertActionStyle.Cancel, _ => tcs.TrySetResult(null)));

                var viewController = UIApplication.SharedApplication.KeyWindow.GetTopViewController();
                viewController.PresentViewController(alertController, true, null);
            });

            return tcs.Task;

            UIAlertAction CreateDefaultAlertAction(DialogAction alertAction)
            {
                return UIAlertAction.Create(alertAction.Title, UIAlertActionStyle.Default, _ =>
                {
                    tcs.TrySetResult(null);
                    alertAction.Command.Execute(null);
                });
            }
        }
    }
}
