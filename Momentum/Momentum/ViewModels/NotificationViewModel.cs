using Microsoft.WindowsAzure.MobileServices;
using Momentum.Extensions;
using Momentum.Helpers;
using Momentum.Models;
using Momentum.Services;
using Momentum.Views;
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
    public class NotificationViewModel : BaseViewModel
    {
        private Command updateNotificationsCommand;

        public ObservableCollection<IThanks> Notifications
        {
            get;
            set;
        }

        public Command UpdateNotificationsCommand
        {
            get { return updateNotificationsCommand ?? (updateNotificationsCommand = new Command(async () => await LoadNotificationsAsync())); }
        }

        public NotificationViewModel()
        {
            Notifications = new ObservableCollection<IThanks>();
        }

        public async Task LoadNotificationsAsync()
        {
            if (IsBusy) return;
              
            IsBusy = true;

            try
            {
                if (await ConnectivityService.IsConnected())
                {
                    //Notifications.Clear();

                    var thanks = await AmazonWebService.Instance.GetUserThanksAsync();

                    Notifications.AddRange(thanks);

                    //await AmazonWebService.Instance.DeleteThanksAsync(thanks);
                }
                else
                {
                    DialogService.ShowError("No internet connection");
                }
            }
            catch (Exception ex)
            {
                //Xamarin.Insights.Report(ex);
            }

            IsBusy = false;
        }
    }
}
