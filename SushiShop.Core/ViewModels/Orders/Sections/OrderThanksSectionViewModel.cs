using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Models.Orders;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SushiShop.Core.ViewModels.Orders.Sections
{
    public class OrderThanksSectionViewModel : BaseViewModel
    {
        public OrderThanksSectionViewModel(
            OrderConfirmationInfo orderConfirmationInfo,
            long orderNumber,
            Func<Task> goToRootFunc)
        {
            Image = orderConfirmationInfo.Image;
            Title = orderConfirmationInfo.Title;
            Content = orderConfirmationInfo.Content;
            OrderNumber = orderNumber;
            OrderNumberTitle = orderConfirmationInfo.OrderNumberTitle;

            GoToRootCommand = new SafeAsyncCommand(ExecutionStateWrapper, goToRootFunc);
        }

        public ICommand GoToRootCommand { get; }

        public string? Image { get; }

        public string? Title { get; }

        public string? Content { get; }

        public string? OrderNumberTitle { get; }

        public long OrderNumber { get; }
    }
}