using System.Windows.Input;

namespace SushiShop.Core.Data.Models.Plugins
{
    public class DialogAction
    {
        public DialogAction(string title, ICommand command)
        {
            Title = title;
            Command = command;
        }

        public string Title { get; }

        public ICommand Command { get; }
    }
}
