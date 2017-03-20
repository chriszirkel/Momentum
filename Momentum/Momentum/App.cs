using Momentum.Helpers;
using Momentum.Models;
using Momentum.Platform;
using Momentum.Primitives;
using Momentum.Services;
using Momentum.Views;
using Plugin.Geolocator;
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
        private static CarouselPage carouselPage;
        private static MomentListPage momentListPage;
        //private static CameraPreviewPage cameraPreviewPage;
        //private static CameraPage cameraPage;
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
            carouselPage.CurrentPage = momentListPage;
        }

        public NavigationPage FetchMainUI()
        {
            momentListPage = new MomentListPage();
            //cameraPage =  new CameraPage();
            //cameraPreviewPage = new CameraPreviewPage();
            profilePage = new ProfilePage();

            carouselPage = new CarouselPage
            {
                Children = {
                    momentListPage,
                    //cameraPage,
                    //cameraPreviewPage,
                    profilePage
                },
                CurrentPage = momentListPage,
            };

            var navigationPage = new NavigationPage(carouselPage);

            NavigationPage.SetHasNavigationBar(momentListPage, true);
            //NavigationPage.SetHasNavigationBar(cameraPage, true);

            //NavigationPage.SetHasNavigationBar(carouselPage, false);
            //{
            //    BarBackgroundColor = Colors.NavigationBarColor,
            //    BarTextColor = Colors.NavigationBarTextColor
            //};

            carouselPage.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
            {
                if (e.PropertyName == "CurrentPage")
                {
                    if (carouselPage.CurrentPage == momentListPage)
                    {
                        //NavigationPage.SetHasNavigationBar(carouselPage, true);
                        //carouselPage.Title = "Moments";
                    }
                    //else if (carouselPage.CurrentPage == cameraPage)
                    //{
                    //    //NavigationPage.SetHasNavigationBar(carouselPage, false);
                    //    //carouselPage.Title = "Camera";
                    //}
                    else if (carouselPage.CurrentPage == profilePage)
                    {
                        //NavigationPage.SetHasNavigationBar(carouselPage, true);
                        //carouselPage.Title = "Profile";
                        //await profilePage.ViewModel.InitializeAsync();
                    }

                    //var currentPageType = carouselPage.CurrentPage.GetType();

                    //if (currentPageType == typeof(MomentListPage))
                    //{
                    //    //NavigationPage.SetHasNavigationBar(carouselPage, true);
                    //    //carouselPage.Title = "Moments";
                    //}
                    //else if (currentPageType == typeof(CameraPage))
                    //{
                    //    //NavigationPage.SetHasNavigationBar(carouselPage, false);
                    //    //carouselPage.Title = "Camera";
                    //}
                    //else if (currentPageType == typeof(ProfilePage))
                    //{
                    //    //NavigationPage.SetHasNavigationBar(carouselPage, true);
                    //    //carouselPage.Title = "Profile";

                    //}
                }
            };

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
