using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Momentum.Helpers
{
    public static class AsyncImageSource
    {
        public static NotifyTask<ImageSource> FromTask(Task<ImageSource> task)
        {
            return NotifyTask.Create(task);
        }

        public static NotifyTask<ImageSource> FromTask(Task<ImageSource> task, ImageSource defaultSource)
        {
            return NotifyTask.Create(task, defaultSource);
        }

        public static NotifyTask<ImageSource> FromUriAndResource(string uri, string resource)
        {
            var u = new Uri(uri);
            return FromUriAndResource(u, resource);
        }

        public static NotifyTask<ImageSource> FromUriAndResource(Uri uri, string resource)
        {
            var t = Task.Run(() => ImageSource.FromUri(uri));
            var defaultResouce = ImageSource.FromResource(resource);

            return FromTask(t, defaultResouce);
        }

        public static NotifyTask<ImageSource> FromFileAndResource(string file)
        {
            var t = Task.Run(() => ImageSource.FromFile(file));
            //var defaultResouce = ImageSource.FromResource(resource);

            return FromTask(t);
        }
    }
}
