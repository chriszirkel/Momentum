using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Momentum.Platform
{
    public interface IFacebook
    {
        bool IsLoggedIn { get; }

        bool IsLoginExpired { get; }

        string AccessToken { get; }

        string GetProfilePictureUri(int width, int height);
    }

    public static class Facebook
    {
        private static IFacebook instance;

        public static IFacebook Instance
        {
            get { return instance ?? (instance = DependencyService.Get<IFacebook>()); }
        }
    }
}
