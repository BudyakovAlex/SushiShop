using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Simple;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Shops;
using SushiShop.Core.ViewModels.Common.Items;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SushiShop.Core.ViewModels.Shops.Items
{
    public class ShopItemViewModel : SelectableItemViewModel<Shop>
    {
        public ShopItemViewModel(Shop key, bool isSelected = false) : base(key.LongTitle, key, isSelected)
        {
            Photos = new MvxObservableCollection<PhotoItemViewModel>(ProducePhotoViewModels());

            GoToMapCommand = new MvxAsyncCommand(GoToMapAsync);
        }

        public ICommand GoToMapCommand { get; }

        public MvxObservableCollection<PhotoItemViewModel> Photos { get; }

        public double Latitude => Key.Latitude;

        public double Longitude => Key.Longitude;

        public string? Phone => Key.Phone;

        public string? WorkingTime => GetWorkingTimeTitle();

        //TODO: wait for BE
        public string? DriveWay => string.Empty;

        private string? GetWorkingTimeTitle()
        {
            return Key.ShopWorkingInfo?.DayOfWeek switch
            {
                DayOfWeek.Friday => Key.FridayWorkingTime,
                DayOfWeek.Monday => Key.MondayWorkingTime,
                DayOfWeek.Saturday => Key.SaturdayWorkingTime,
                DayOfWeek.Sunday => Key.SundayWorkingTime,
                DayOfWeek.Thursday => Key.ThursdayWorkingTime,
                DayOfWeek.Tuesday => Key.TuesdayWorkingTime,
                DayOfWeek.Wednesday => Key.WednesdayWorkingTime,
                _ => string.Empty
            };
        }

        private PhotoItemViewModel[] ProducePhotoViewModels()
        {
            var viewModels = Key.Images.Select(item => new PhotoItemViewModel(item.JpgUrl!, null)).ToArray();
            return viewModels;
        }

        private Task GoToMapAsync()
        {
            return NavigationManager.NavigateAsync<ShopOnMapViewModel>();
        }
    }
}