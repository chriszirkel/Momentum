using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Acr.UserDialogs;
using Xamarin.Forms;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Momentum.Services;
using Xamarin.Facebook;
using Xamarin.Facebook.AppEvents;
using Xamarin.Facebook.Login;
using Android.Content;
using ImageCircle.Forms.Plugin.Droid;
using Xamarin.Forms.Platform.Android;

namespace Momentum.Droid
{
    [Activity(Label = "Momentum", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity // global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        //private DroidPlatform platform;
        //private ICallbackManager callbackManager;
        //private FacebookCallback facebookCallback;// = new FacebookCallback();

        internal ICallbackManager FacebookCallbackManager
        {
            get;
            private set;
        }

        //internal FacebookCallback FacebookCallback
        //{
        //    get { return facebookCallback; }
        //}

        public static MainActivity Instance
        {
            get;
            private set;
        }

        //private void CreateAndShowDialog(string message, string title)
        //{
        //    AlertDialog.Builder builder = new AlertDialog.Builder(this);

        //    builder.SetMessage(message);
        //    builder.SetTitle(title);
        //    builder.Create().Show();
        //}

        protected override void OnCreate(Bundle bundle)
        {
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            Instance = this;

            App.ScreenWidth = (int)Resources.DisplayMetrics.WidthPixels; // real pixels
            App.ScreenHeight = (int)Resources.DisplayMetrics.HeightPixels; // real pixels

            FacebookSdk.SdkInitialize(ApplicationContext);
            AppEventsLogger.ActivateApp(Application);

            FacebookCallbackManager = CallbackManagerFactory.Create();
            //var facebookCallback = new FacebookCallback()
            //{
            //    HandleSuccess = (Xamarin.Facebook.Login.LoginResult loginResult) =>
            //    {
            //        //formsButton.LoginSuccess(loginResult.AccessToken.Token);
            //    },

            //    HandleCancel = () =>
            //    {
            //        // App code
            //    },

            //    HandleError = (FacebookException exception) =>
            //    {
            //        // App code
            //    }
            //};

            //LoginManager.Instance.RegisterCallback(FacebookCallbackManager, facebookCallback);
            //LoginManager.Instance.LogInWithReadPermissions(MainActivity.Instance, new[] { "public_profile" });


            //this.Window.AddFlags(WindowManagerFlags.Fullscreen);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            //Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            UserDialogs.Init(() => (Activity)Forms.Context);
            ImageCircleRenderer.Init();

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            FacebookCallbackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }

        //internal void SetPlatformCallback(DroidPlatform platform)
        //{
        //    //facebookCallback.platform = platform;
        //    this.platform = platform;
        //}
    }

    //public class FacebookCallback : Java.Lang.Object, IFacebookCallback
    //{
    //    internal DroidPlatform platform;

    //    public void OnCancel()
    //    {
    //        platform.OnFacebookLoginCancel();
    //    }

    //    public void OnError(FacebookException e)
    //    {
    //        platform.OnFacebookLoginError(e);
    //    }

    //    public async void OnSuccess(Java.Lang.Object obj)
    //    {
    //        Xamarin.Facebook.Login.LoginResult loginResult = (Xamarin.Facebook.Login.LoginResult)obj;
    //        await platform.OnFacebookLoginSuccess(loginResult.AccessToken.Token);
    //    }
    //}
}

