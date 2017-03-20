using Momentum.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Momentum.Views
{
    public partial class NotificationPage : ContentPage
    {
        private NotificationViewModel ViewModel
        {
            get { return BindingContext as NotificationViewModel; }
        }

        public NotificationPage()
        {
            BindingContext = new NotificationViewModel();

            InitializeComponent();
        }

        private void SetupEventHandlers()
        {
            NotificationListView.Refreshing += async (s, e) =>
            {
                NotificationListView.IsRefreshing = true;
                await ViewModel.LoadNotificationsAsync();
                NotificationListView.IsRefreshing = false;
            };

            //NotificationListView.ItemSelected += (s, e) =>
            //{
            //    NotificationListView.SelectedItem = null;
            //};

            //NotificationListView.ItemTapped += (s, e) =>
            //{
            //    var moment = e.Item as Moment;

            //    if (moment == null)
            //        return;

            //    App.Current.MainPage.Navigation.PushModalAsync(new MomentViewPage(moment));
            //    //ViewModel.DestroyImageCommand.Execute(moment);
            //    NotificationListView.SelectedItem = null;
            //};
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel == null || ViewModel.IsBusy || ViewModel.Notifications.Count > 0)
            {
                return;
            }

            await ViewModel.LoadNotificationsAsync();
        }
    }
}
