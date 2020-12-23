using System.Threading.Tasks;
using SushiShop.Core.Data.Models.Plugins;

namespace SushiShop.Core.Plugins
{
    public interface IDialog
    {
        Task ShowActionSheetAsync(string? title, string? message, string cancelTitle, params DialogAction[] actions);
    }
}
