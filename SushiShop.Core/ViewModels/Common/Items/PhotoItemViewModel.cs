using System;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.NavigationParameters;

namespace SushiShop.Core.ViewModels.Common.Items
{
    public class PhotoItemViewModel : BaseViewModel
    {
        private string[] allImages;

        public PhotoItemViewModel(string imagePath,
                                  string[] allImages,
                                  Action<PhotoItemViewModel>? removeAction = null,
                                  bool canRemove = true)
        {
            this.allImages = allImages;
            ImagePath = imagePath;
            CanRemove = canRemove;

            RemoveCommand = new MvxCommand(() => removeAction?.Invoke(this));
            ShowDetailsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowDetailsAsync);
        }

        public void SetAllImages(string[] allImages) =>
            this.allImages = allImages;

        public IMvxCommand RemoveCommand { get; }
        public IMvxCommand ShowDetailsCommand { get; }

        public string ImagePath { get; }

        public bool CanRemove { get; }

        private Task ShowDetailsAsync()
        {
            var navigationParams = new PhotoDetailsNavigationParams(allImages, ImagePath);
            return NavigationManager.NavigateAsync<PhotoDetailsViewModel, PhotoDetailsNavigationParams>(navigationParams);
        }
    }
}