using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Simple;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Shops;
using SushiShop.Core.ViewModels.Common.Items;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace SushiShop.Core.ViewModels.Shops.Items
{
    public class ShopItemViewModel : SelectableItemViewModel<Shop>
    {
        private readonly Func<Shop, Task> goToMapFunc;
        private readonly Func<Shop, Task> confirmSelectionFunc;

        public ShopItemViewModel(
            Shop key,
            Func<Shop, Task> goToMapFunc,
            Func<Shop, Task> confirmSelectionFunc,
            bool isSelectionMode,
            bool isSelected = false,
            Action<Shop>? showNearestMetroAction = null) : base(key.LongTitle, key, isSelected)
        {
            this.goToMapFunc = goToMapFunc;
            this.confirmSelectionFunc = confirmSelectionFunc;

            IsSelectionMode = isSelectionMode;
            Photos = new MvxObservableCollection<PhotoItemViewModel>(ProducePhotoViewModels());

            GoToMapCommand = new MvxAsyncCommand(GoToMapAsync);
            ShowNearestMetroCommand = new MvxCommand(() => showNearestMetroAction?.Invoke(Key));
            ConfirmSelectionCommand = new MvxAsyncCommand(ConfirmSelectionAsync, () => IsSelectionMode);
            CallCommand = new MvxCommand<string>(PhoneDialer.Open);
        }

        public ICommand GoToMapCommand { get; }

        public ICommand ShowNearestMetroCommand { get; }

        public ICommand ConfirmSelectionCommand { get; }

        public ICommand CallCommand { get; }

        public MvxObservableCollection<PhotoItemViewModel> Photos { get; }

        public bool HasPhotos => Photos.IsNotEmpty();

        public double Latitude => Key.Coordinates.Latitude ?? 0;

        public double Longitude => Key.Coordinates.Longitude ?? 0;

        public string? Phone => Key.Phone;

        public string[] Phones => Key.Phones;

        public string? WorkingTime => GetWorkingTimeTitle();

        public string? DriveWay => Key.DriveWay;

        public bool HasDriveWay => DriveWay.IsNotNullNorEmpty();

        public string? LongTitle => Key.LongTitle;

        public bool HasNearestMetro => Key.Metro.Any();

        public bool IsSelectionMode { get; }

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
            var viewModels = Key.Images.Select(item => new PhotoItemViewModel(item.JpgUrl!, canRemove: false)).ToArray();
            return viewModels;
        }

        private Task GoToMapAsync()
        {
            return goToMapFunc?.Invoke(Key) ?? Task.CompletedTask;
        }

        private Task ConfirmSelectionAsync()
        {
            return confirmSelectionFunc?.Invoke(Key) ?? Task.CompletedTask;
        }
    }
}