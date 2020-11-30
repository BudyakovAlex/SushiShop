using MvvmCross.Plugin.Messenger;

namespace SushiShop.Core.Messages
{
    public class CartProductChangedMessage : MvxMessage
    {
        public CartProductChangedMessage(object sender) : base(sender)
        {
        }
    }
}