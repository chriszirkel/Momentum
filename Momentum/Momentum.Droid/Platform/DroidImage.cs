using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Momentum.Services;
using Android.Graphics;
using System.IO;
using Momentum.Platform;

[assembly: Xamarin.Forms.Dependency(typeof(Momentum.Droid.Platform.DroidImage))]
namespace Momentum.Droid.Platform
{
    public class DroidImage : IImage
    {
        public async Task<byte[]> ResizeImage(byte[] imageData, float width, float height, int quality = 100)
        {
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);

            using (MemoryStream ms = new MemoryStream())
            {
                await resizedImage.CompressAsync(Bitmap.CompressFormat.Jpeg, quality, ms);
                return ms.ToArray();
            }
        }

        public async Task<byte[]> ScaleImage(byte[] imageData, float maxWidth, float maxHeight, int quality = 100)
        {
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);

            float actualHeight = originalImage.Height;
            float actualWidth = originalImage.Width;
            float imgRatio = actualWidth / actualHeight;
            float maxRatio = maxWidth / maxHeight;

            if (actualHeight > maxHeight || actualWidth > maxWidth)
            {
                if (imgRatio < maxRatio)
                {
                    //adjust width according to maxHeight 
                    imgRatio = maxHeight / actualHeight;
                    actualWidth = imgRatio * actualWidth;
                    actualHeight = maxHeight;
                }
                else if (imgRatio > maxRatio)
                {
                    //adjust height according to maxWidth 
                    imgRatio = maxWidth / actualWidth;
                    actualHeight = imgRatio * actualHeight;
                    actualWidth = maxWidth;
                }
                else
                {
                    actualHeight = maxHeight;
                    actualWidth = maxWidth;
                }
            }
            else
            {
                // keep quality...
            }

            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)actualWidth, (int)actualHeight, false);

            using (MemoryStream ms = new MemoryStream())
            {
                await resizedImage.CompressAsync(Bitmap.CompressFormat.Jpeg, quality, ms);
                return ms.ToArray();
            }
        }
    }
}