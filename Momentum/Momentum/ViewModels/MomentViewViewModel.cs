using Momentum.Models;
using Momentum.Primitives;
using Momentum.Services;
using PropertyChanged;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Momentum.ViewModels
{
    [ImplementPropertyChanged]
    public class MomentViewViewModel : BaseViewModel
    {
        private Command thanksCommand;
        private Command reportCommand;

        private Moment moment;
        private IUser user;

        public string MomentImageSource
        {
            get;
            private set;
        }

        public Address MomentAddress
        {
            get;
            private set;
        }

        public Command ThanksCommand
        {
            get { return thanksCommand ?? (thanksCommand = new Command(async () => await SendThanksAsync())); }
        }

        public Command ReportCommand
        {
            get { return reportCommand ?? (reportCommand = new Command(async () => await SendReportAsync())); }
        }

        public MomentViewViewModel(Moment moment)
        {
            this.moment = moment;
        }

        public async Task InitializeAsync()
        {
            await LoadUserAsync();
            await LoadMomentAsync();
        }

        //public async Task OnThanksClicked()
        //{
        //    await SendThanksAsync();
        //}

        //public async Task OnReportClicked()
        //{
        //    await SendReportAsync();
        //}

        private async Task LoadUserAsync()
        {
            user = await AmazonWebService.Instance.GetUserAsync();
        }

        private async Task LoadMomentAsync()
        {
            //IsBusy = true;

            //var stream = await AmazonWebService.Instance.LoadMomentAsync(Moment);
            //Image = ImageSource.FromStream( () => stream);

            MomentImageSource = AmazonWebService.Instance.GetMomentUrl(moment);
            MomentAddress = moment.Address;

            user.ReceivedMomentsCount++;
            await AmazonWebService.Instance.SaveUserAsync(user);

            //IsBusy = false;
        }

        private async Task SendThanksAsync()
        {
            var thanks = Thanks.Create(moment);
            await AmazonWebService.Instance.SaveThanksAsync(thanks);

            user.SentThanksCount++;
            await AmazonWebService.Instance.SaveUserAsync(user);

            DialogService.ShowSuccess("Thanks sent!");
        }

        private async Task SendReportAsync()
        {
            //var report = new Report();
            //report.MomentId = moment.MomentId;
            //report.UserId = App.CurrentUser.UserId;
            //...
            //await AmazonWebService.Instance.SaveReportAsync(report);

            //DialogService.ShowSuccess("Report sent!");
        }
    }
}
