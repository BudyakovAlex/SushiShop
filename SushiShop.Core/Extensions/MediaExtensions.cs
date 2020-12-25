using System.Threading.Tasks;
using Plugin.Media.Abstractions;

namespace SushiShop.Core.Extensions
{
    public static class MediaExtensions
    {
        public static Task<MediaFile?> PickPhotoOrDefaultAsync(this IMedia media)
        {
            if (!media.IsPickPhotoSupported)
            {
                return Task.FromResult<MediaFile?>(null);
            }

            return media.PickPhotoAsync();
        }

        public static Task<MediaFile?> TakePhotoOrDefaultAsync(this IMedia media)
        {
            if (!media.IsCameraAvailable || !media.IsTakePhotoSupported)
            {
                return Task.FromResult<MediaFile?>(null);
            }

            return media.TakePhotoAsync(new StoreCameraMediaOptions());
        }
    }
}
