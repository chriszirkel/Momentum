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
using Java.Lang;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;

namespace Momentum.Droid
{
    public class FacebookCallback : Java.Lang.Object, IFacebookCallback
    {
        public Action HandleCancel { get; set; }
        public Action<FacebookException> HandleError { get; set; }
        public Action<LoginResult> HandleSuccess { get; set; }

        public void OnCancel()
        {
            var action = HandleCancel;

            if (action != null)
                action();
        }

        public void OnError(FacebookException e)
        {
            var action = HandleError;

            if (action != null)
                action(e);
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            var action = HandleSuccess;

            if (action != null)
                action((LoginResult)result);
        }
    }
}