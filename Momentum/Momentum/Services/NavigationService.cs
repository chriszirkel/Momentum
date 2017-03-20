using Momentum.Primitives;
using Momentum.Views;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Momentum.Services
{
    public class NavigationService
    {
        private static NavigationService instance;

        private bool isNavigating;

        public static NavigationService Instance
        {
            get
            {
                return instance ?? (instance = new NavigationService());
            }
        }

        private NavigationService()
        {

        }

        public async Task ShowNotificationsAsync()
        {
            if (isNavigating)
                return;

            isNavigating = true;

            await App.Instance.MainPage.Navigation.PushModalAsync(new NotificationPage());

            isNavigating = false;
        }

        public async Task ShowSettingsAsync()
        {
            if (isNavigating)
                return;

            isNavigating = true;

            await App.Instance.MainPage.Navigation.PushModalAsync(new SettingsPage());

            isNavigating = false;
        }

        public async Task ShowMomentPreviewAsync(byte[] image)
        {
            await App.Instance.MainPage.Navigation.PushModalAsync(new MomentPreviewPage(image), false);
        }

        public async Task ShowMomentListAsync()
        {
            App.Instance.NavigateToMomentList();

            if(App.Instance.MainPage.Navigation.ModalStack.Count != 0)
                await App.Instance.MainPage.Navigation.PopModalAsync();
        }
    }
}
