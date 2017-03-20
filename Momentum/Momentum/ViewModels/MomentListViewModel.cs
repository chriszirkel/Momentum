
using Momentum.Models;
using Momentum.Services;
using Momentum.Extensions;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace Momentum.ViewModels
{
    [ImplementPropertyChanged]
    public class MomentListViewModel : BaseViewModel
    {
        private Command updateMomentsCommand;

        public ObservableCollection<IMoment> Moments
        {
            get;
            set;
        }

        public Command UpdateMomentsCommand
        {
            get { return updateMomentsCommand ?? (updateMomentsCommand = new Command(async () => await UpdateMomentsAsync())); }
        }

        public MomentListViewModel()
        {
            Moments = new ObservableCollection<IMoment>();
        }

        public async Task UpdateMomentsAsync()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                if (await ConnectivityService.IsConnected())
                {
                    Moments.Clear();

                    //var items = await AzureService.Instance.GetItemsAsync(x => x.CountryCode == Country.CountryCode);
                    var moments = await AmazonWebService.Instance.GetLatestMomentsAsync();

                    Moments.AddRange(moments);
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

            //UserDialogs.Instance.Toast(new ToastConfig(new ToastEvent(), "Test", "Testing toast functionality....fun!")
            //{
            //    Duration = TimeSpan.FromSeconds(3)
            //});

            IsBusy = false;
        }
    }
}
