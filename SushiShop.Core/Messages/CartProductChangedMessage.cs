using MvvmCross.Plugin.Messenger;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Cart;

namespace SushiShop.Core.Messages
{
    public class CartProductChangedMessage : MvxMessage
    {
        public CartProductChangedMessage(
            object sender,
            ProductChangeAction productChangeAction,
            CartProduct cartProduct) : base(sender)
        {
            ProductChangeAction = productChangeAction;
            CartProduct = cartProduct;
        }

        public ProductChangeAction ProductChangeAction { get; }

        public CartProduct CartProduct { get; }
    }
}