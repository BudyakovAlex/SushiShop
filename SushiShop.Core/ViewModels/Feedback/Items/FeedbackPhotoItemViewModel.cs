using System;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.ViewModels.Common;

namespace SushiShop.Core.ViewModels.Feedback.Items
{
    public class FeedbackPhotoItemViewModel : BaseViewModel
    {
        public FeedbackPhotoItemViewModel(string imagePath, Action<FeedbackPhotoItemViewModel> removeAction)
        {
            ImagePath = imagePath;

            RemoveCommand = new MvxCommand(() => removeAction.Invoke(this));
            ShowDetailsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowDetailsAsync);
        }

        public IMvxCommand RemoveCommand { get; }
        public IMvxCommand ShowDetailsCommand { get; }

        public string ImagePath { get; }

        private Task ShowDetailsAsync()
        {
            return NavigationManager.NavigateAsync<PhotoDetailsViewModel, string>(ImagePath);
        }
    }
}