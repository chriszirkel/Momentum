using Microsoft.WindowsAzure.MobileServices;
using Momentum.Extensions;
using Momentum.Helpers;
using Momentum.Models;
using Momentum.Platform;
using Momentum.Services;
using Momentum.Views;
using Plugin.Media;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Momentum.ViewModels
{
    [ImplementPropertyChanged]
    public class ProfileViewModel : BaseViewModel
    {
        private Command settingsCommand;
        private Command notificationsCommand;
        private Command debugCommand;
        private bool isInitialized;

        public Command SettingsCommand
        {
            get { return settingsCommand ?? (settingsCommand = new Command(async () => await NavigationService.Instance.ShowSettingsAsync())); }
        }

        public Command NotificationsCommand
        {
            get { return notificationsCommand ?? (notificationsCommand = new Command(async () => await ExecuteNotificationsCommand())); }
        }

        public Command DebugCommand
        {
            get { return debugCommand ?? (debugCommand = new Command(async () => await ExecuteDebugCommand())); }
        }

        public ObservableCollection<NotifyTask<ImageSource>> SentMoments
        {
            get;
            set;
        }

        public string ProfileImageSource
        {
            get;
            private set;
        }

        public int SentMomentsCount
        {
            get;
            private set;
        }

        public int ReceivedMomentsCount
        {
            get;
            private set;
        }

        public int SentThanksCount
        {
            get;
            private set;
        }

        public int ReceivedThanksCount
        {
            get;
            private set;
        }

        public ProfileViewModel()
        {
            SentMoments = new ObservableCollection<NotifyTask<ImageSource>>();
        }

        //private async Task ExecuteSettingsCommand()
        //{
        //    //if (IsBusy)
        //    //{
        //    //    return;
        //    //}

        //    //IsBusy = true;

        //    await App.Current.MainPage.Navigation.PushModalAsync(new SettingsPage());

        //    //IsBusy = false;
        //}

        private async Task ExecuteNotificationsCommand()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new NotificationPage());
        }

        private async Task ExecuteDebugCommand()
        {
            //var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            //{
            //    Directory = "Sample",
            //    Name = "test.jpg"
            //});


        }

        //public async override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    await LoadUserAsync();
        //    await LoadUserMomentsAsync();
        //}

        public async Task InitializeAsync()
        {
            if (isInitialized)
                return;

            await LoadUserAsync();
            await LoadSentMomentsAsync();

            isInitialized = true;
        }

        private async Task LoadUserAsync()
        {
            ProfileImageSource = Facebook.Instance.GetProfilePictureUri(150, 150);

            var user = await AmazonWebService.Instance.GetUserAsync();

            SentMomentsCount = user.SentMomentsCount;
            ReceivedMomentsCount = user.ReceivedMomentsCount;
            SentThanksCount = user.SentThanksCount;
            ReceivedThanksCount = user.ReceivedThanksCount;
        }

        private async Task LoadSentMomentsAsync()
        {
            var moments = await LocalDataService.Instance.GetSentMomentsAsync();

            foreach(var moment in moments)
            {
                SentMoments.Add(AsyncImageSource.FromFileAndResource(moment.ImageSource));
            }
        }
    }
}
