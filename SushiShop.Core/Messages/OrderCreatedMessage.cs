using MvvmCross.Plugin.Messenger;

namespace SushiShop.Core.Messages
{
    public class OrderCreatedMessage : MvxMessage
    {
        public OrderCreatedMessage(object sender) : base(sender)
        {
        }
    }
}
