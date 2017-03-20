using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Momentum.Services;
using Xamarin.Facebook;
using Momentum.Platform;

[assembly: Xamarin.Forms.Dependency(typeof(Momentum.Droid.Platform.DroidFacebook))]
namespace Momentum.Droid.Platform
{
    public class DroidFacebook : IFacebook
    {
        public string AccessToken
        {
            get
            {
                return Xamarin.Facebook.AccessToken.CurrentAccessToken?.Token;
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return !string.IsNullOrEmpty(AccessToken);
            }
        }

        public bool IsLoginExpired
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string GetProfilePictureUri(int width, int height)
        {
            return Profile.CurrentProfile.GetProfilePictureUri(width, height).ToString();
        }
    }
}