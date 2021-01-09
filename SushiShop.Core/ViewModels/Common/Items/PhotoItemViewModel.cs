using System;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;

namespace SushiShop.Core.ViewModels.Common.Items
{
    public class PhotoItemViewModel : BaseViewModel
    {
        public PhotoItemViewModel(string imagePath, Action<PhotoItemViewModel>? removeAction = null, bool canRemove = true)
        {
            ImagePath = imagePath;
            CanRemove = canRemove;

            RemoveCommand = new MvxCommand(() => removeAction?.Invoke(this));
            ShowDetailsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowDetailsAsync);
        }

        public IMvxCommand RemoveCommand { get; }
        public IMvxCommand ShowDetailsCommand { get; }

        public string ImagePath { get; }

        public bool CanRemove { get; }

        private Task ShowDetailsAsync()
        {
            return NavigationManager.NavigateAsync<PhotoDetailsViewModel, string>(ImagePath);
        }
    }
}