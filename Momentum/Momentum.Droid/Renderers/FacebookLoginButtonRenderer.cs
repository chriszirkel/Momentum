using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Xamarin.Forms;
using Momentum.Controls;
using Momentum.Droid.Renderers;
using Xamarin.Forms.Platform.Android;
using Xamarin.Facebook.Login.Widget;
using Xamarin.Facebook.Login;
using Xamarin.Facebook;

[assembly: ExportRenderer(typeof(FacebookLoginButton), typeof(FacebookLoginButtonRenderer))]
namespace Momentum.Droid.Renderers
{
    public class FacebookLoginButtonRenderer : ViewRenderer<Button, LoginButton>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            var activity = Context as MainActivity;

            var formsButton = e.NewElement as FacebookLoginButton;

            var loginButton = new LoginButton(Context);
            loginButton.SetReadPermissions("public_profile");

            var facebookCallback = new FacebookCallback()
            {
                HandleSuccess = (LoginResult loginResult) =>
                {
                    formsButton.OnLoginSuccess(loginResult.AccessToken.Token);
                },

                HandleCancel = () =>
                {
                    formsButton.OnLoginCancel();
                },

                HandleError = (FacebookException exception) =>
                {
                    formsButton.OnLoginError(exception.Message);
                }
            };

            loginButton.RegisterCallback(activity.FacebookCallbackManager, facebookCallback);
            SetNativeControl(loginButton);
        }
    }
}