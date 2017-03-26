//using BottomBar.XamarinForms;
//using Momentum.Controls;
using BottomBar.XamarinForms;
using Momentum.Helpers;
using Momentum.Models;
using Momentum.Platform;
using Momentum.Primitives;
using Momentum.Services;
using Momentum.Views;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Momentum
{
    public class App : Application
    {
        private static BottomBarPage bottomBarPage;
        private static MomentListPage momentListPage;
        private static CameraPage cameraPage;
        private static ProfilePage profilePage;

        public static int ScreenWidth;
        public static int ScreenHeight;

        public static App Instance
        {
            get { return Current as App; }
        }

        public static IUser CurrentUser { get; set; }

        public Position CurrentPosition
        {
            get;
            private set;
        }

        public Address CurrentAddress
        {
            get;
            private set;
        }

        public App()
        {
            Label label = new Label() { Text = "Loading..." };
            label.TextColor = Color.White;

            //Image img = new Image() { Source = ImageSource.FromFile("logo.png") };

            StackLayout stack = new StackLayout();
            stack.VerticalOptions = LayoutOptions.Center;
            stack.HorizontalOptions = LayoutOptions.Center;
            stack.Orientation = StackOrientation.Vertical;
            //stack.Children.Add(img);
            stack.Children.Add(label);

            ContentPage page = new ContentPage();
            page.BackgroundColor = Color.FromHex("#8C0A4B");
            page.Content = stack;

            MainPage = page;
        }

        public void NavigateToMainUI()
        {
            MainPage = FetchMainUI();
        }

        public void NavigateToMomentList()
        {
            bottomBarPage.CurrentPage = momentListPage;
        }

        public NavigationPage FetchMainUI()
        {
            momentListPage = new MomentListPage();
            cameraPage = new CameraPage();
            //cameraPreviewPage = new CameraPreviewPage();
            profilePage = new ProfilePage();

            bottomBarPage = new BottomBarPage();
            bottomBarPage.BarBackgroundColor = Color.Pink;

            bottomBarPage.Children.Add(momentListPage);
            momentListPage.Title = "Moments";
            momentListPage.Icon = (FileImageSource)ImageSource.FromFile("earth.png");
            //momentListPage.SetTabColor(Color.FromHex("#5D4037"));

            bottomBarPage.Children.Add(cameraPage);
            cameraPage.Title = "Camera";
            cameraPage.Icon = (FileImageSource)ImageSource.FromFile("camera.png");
            //cameraPage.SetTabColor(Color.FromHex("#5D4037"));

            bottomBarPage.Children.Add(profilePage);
            profilePage.Title = "Profile";
            profilePage.Icon = (FileImageSource)ImageSource.FromFile("face.png");
            //profilePage.SetTabColor(Color.FromHex("#5D4037"));

            
            var navigationPage = new NavigationPage(bottomBarPage);
            NavigationPage.SetHasNavigationBar(bottomBarPage, false);
            //NavigationPage navigationPage = null;

            //string[] tabTitles = { "Favorites", "Friends", "Nearby", "Recents", "Restaurants" };
            //string[] tabColors = { null, "#5D4037", "#7B1FA2", "#FF5252", "#FF9800" };

            //for (int i = 0; i < tabTitles.Length; ++i)
            //{
            //    string title = tabTitles[i];
            //    string tabColor = tabColors[i];

            //FileImageSource icon = (FileImageSource)ImageSource.FromFile(string.Format("ic_{0}.png", title.ToLowerInvariant()));

            //    // create tab page
            //    var tabPage = new TabPage()
            //    {
            //        Title = title,
            //        Icon = icon
            //    };

            //    // set tab color
            //    if (tabColor != null)
            //    {
            //        tabPage.SetTabColor(Color.FromHex(tabColor));
            //    }

            //    // set label based on title
            //    tabPage.UpdateLabel();

            //    // add tab pag to tab control
            //    bottomBarPage.Children.Add(tabPage);
            //}

            return navigationPage;
        }

        protected override async void OnStart()
        {
            if (Facebook.Instance.IsLoggedIn)
            {
                AmazonWebService.Instance.AddFacebookLogin(Facebook.Instance.AccessToken);

                MainPage = FetchMainUI();

                CurrentUser = await AmazonWebService.Instance.GetUserAsync();
            }
            else
            {
                MainPage = new SignInPage();
                //MainPage = FetchMainUI();
            }

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                    {
                        //await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
                    status = results[Permission.Storage];
                }

                if (status == PermissionStatus.Granted)
                {
                    //var results = await CrossGeolocator.Current.GetPositionAsync(10000);
                    //LabelGeolocation.Text = "Lat: " + results.Latitude + " Long: " + results.Longitude;
                }
                else if (status != PermissionStatus.Unknown)
                {
                    //await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {

                //LabelGeolocation.Text = "Error: " + ex;
            }

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                    {
                        //await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera });
                    status = results[Permission.Camera];
                }

                if (status == PermissionStatus.Granted)
                {
                    //var results = await CrossGeolocator.Current.GetPositionAsync(10000);
                    //LabelGeolocation.Text = "Lat: " + results.Latitude + " Long: " + results.Longitude;
                }
                else if (status != PermissionStatus.Unknown)
                {
                    //await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {

                //LabelGeolocation.Text = "Error: " + ex;
            }

            await SetupGeolocator();
        }

        protected override void OnSleep()
        {

        }

        protected override async void OnResume()
        {
            await SetupGeolocator();
        }

        private async Task SetupGeolocator()
        {
            if (Location.Instance.HasGeolocation)
            {
                CurrentPosition = await Location.Instance.GetPositionAsync();
                CurrentAddress = await Location.Instance.GetAddressForPositionAsync(CurrentPosition);
            }
            else
            {

            }
        }

        //private async Task<bool> StartGeolocator()
        //{
        //    return await LocationService.Instance.StartGeolocator();
        //}

        //private async void StopGeolocator()
        //{
        //    await LocationService.Instance.StopGelocator();
        //}

        public async Task GetPositionAsync()
        {
            //try
            //{
            //    var locator = CrossGeolocator.Current;
            //    locator.DesiredAccuracy = 1000;

            //    if (locator.IsGeolocationAvailable && locator.IsGeolocationEnabled)
            //    {

            //        var position = await locator.GetPositionAsync();

            //        //Latitude.Text = position.Latitude.ToString();
            //        //Longitude.Text = position.Longitude.ToString();

            //        //moment.Latitude = position.Latitude;
            //        //moment.Longitude = position.Longitude;

            //        moment.Position = new Position(position.Latitude, position.Longitude);

            //        var address = await LocationService.Instance.GetAddressForPositionAsync(position.Latitude, position.Longitude);

            //        if (address != null)
            //        {
            //            moment.City = address.City;
            //            moment.CountryCode = address.CountryCode;
            //            moment.CountryName = address.CountryName;
            //        }
            //        else
            //        {
            //            var countryCodes = new List<string>() { "DE", "US", "UK", "FR", "BR", "CH", "GH", "HR", "IS", "NZ" };

            //            moment.City = "New York City";
            //            moment.CountryCode = countryCodes.Random();
            //            moment.CountryName = "TEST";
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("Unable to get location, may need to increase timeout: " + ex);
            //}
        }
    }
}
