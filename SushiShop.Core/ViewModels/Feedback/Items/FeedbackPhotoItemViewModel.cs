using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.ViewModels.Common;
using System;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Feedback.Items
{
    public class FeedbackPhotoItemViewModel : BaseViewModel
    {
        public FeedbackPhotoItemViewModel(string imageUrl, Action<FeedbackPhotoItemViewModel> removeAction)
        {
            ImageUrl = imageUrl;

            RemoveCommand = new MvxCommand(() => removeAction.Invoke(this));
            ShowDetailsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowDetailsAsync);
        }

        public string ImageUrl { get; }

        public IMvxCommand RemoveCommand { get; }

        public IMvxCommand ShowDetailsCommand { get; }

        private Task ShowDetailsAsync()
        {
            return NavigationManager.NavigateAsync<PhotoDetailsViewModel, string>(ImageUrl);
        }
    }
}