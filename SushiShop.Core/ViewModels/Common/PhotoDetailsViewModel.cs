using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Common.Items;
using System;
using System.Linq;

namespace SushiShop.Core.ViewModels.Common
{
    public class PhotoDetailsViewModel : BaseItemsPageViewModel<PhotoDetailsItemViewModel, PhotoDetailsNavigationParams>
    {
        public string Title => AppStrings.Photo;

        public int CurrentIndex { get; private set; }

        public override void Prepare(PhotoDetailsNavigationParams parameters)
        {
            var photos = parameters.Photos.Select(imagePath => new PhotoDetailsItemViewModel(imagePath)).ToArray();

            CurrentIndex = Array.IndexOf(parameters.Photos, parameters.CurrentPhoto);

            Items.ReplaceWith(photos);
        }
    }
}