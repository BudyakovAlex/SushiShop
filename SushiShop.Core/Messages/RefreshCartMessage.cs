using MvvmCross.Plugin.Messenger;

namespace SushiShop.Core.Messages
{
    public class RefreshCartMessage : MvxMessage
    {
        public RefreshCartMessage(object sender) : base(sender)
        {
        }
    }
}