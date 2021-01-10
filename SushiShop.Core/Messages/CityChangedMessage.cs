using MvvmCross.Plugin.Messenger;

namespace SushiShop.Core.Messages
{
    public class CityChangedMessage : MvxMessage
    {
        public CityChangedMessage(object sender) : base(sender)
        {
        }
    }
}