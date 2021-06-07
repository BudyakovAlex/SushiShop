using Android.Content;
using Android.Gms.Maps.Model;
using Android.Graphics;

namespace SushiShop.Droid.Extensions
{
    public static class DrawableExtension
    {
        public static BitmapDescriptor DrawableToBitmapDescriptor(this Context context, int id)
        {
            var vectorDrawable = context.GetDrawable(id);
            vectorDrawable.SetBounds(0, 0, vectorDrawable.IntrinsicWidth, vectorDrawable.IntrinsicHeight);
            var bitmap = Bitmap.CreateBitmap(vectorDrawable.IntrinsicWidth, vectorDrawable.IntrinsicHeight, Bitmap.Config.Argb8888);
            Canvas canvas = new Canvas(bitmap);
            vectorDrawable.Draw(canvas);
            return BitmapDescriptorFactory.FromBitmap(bitmap);
        }
    }
}
