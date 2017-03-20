using Momentum.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Momentum.Platform
{
    public interface IImage
    {
        Task<byte[]> ScaleImage(byte[] imageData, float maxWidth, float maxHeight, int quality = 100);

        Task<byte[]> ResizeImage(byte[] imageData, float width, float height, int quality = 100);
    }

    public class Image
    {
        private static IImage instance;

        public static IImage Instance
        {
            get { return instance ?? (instance = DependencyService.Get<IImage>()); }
        }
    }
}
