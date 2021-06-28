using BuildApps.Core.Mobile.MvvmCross.Managers.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SushiShop.Core.Extensions
{
    public static class NavigationManagerExtensions
    {
        private const int MillisecondsDelay = 600;

        public static async Task<bool> CloseWithDelayAsync<TResult>(this INavigationManager navigationManager, IMvxViewModelResult<TResult> viewModel, TResult result)
            where TResult : notnull
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                await Task.Delay(MillisecondsDelay);
            }

            return await navigationManager.CloseAsync(viewModel, result);
        }

        public static async Task<bool> CloseWithDelayAsync(this INavigationManager navigationManager, IMvxViewModel viewModel)
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                await Task.Delay(MillisecondsDelay);
            }

            return await navigationManager.CloseAsync(viewModel);
        }
    }
}