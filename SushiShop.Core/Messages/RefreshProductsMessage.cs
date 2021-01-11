using MvvmCross.Plugin.Messenger;

namespace SushiShop.Core.Messages
{
    public class RefreshProductsMessage : MvxMessage
    {
        public RefreshProductsMessage(object sender) : base(sender)
        {
        }
    }
}