using Momentum.Models;
using Momentum.Platform;
using Momentum.Services;
using PropertyChanged;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Momentum.ViewModels
{
    [ImplementPropertyChanged]
    public class MomentPreviewViewModel : BaseViewModel
    {
        private Command sendCommand;
        private Command cancelCommand;
        private byte[] image;

        public Command SendCommand
        {
            get { return sendCommand ?? (sendCommand = new Command(async () => await SendMomentAsync())); }
        }

        //public Command CancelCommand
        //{
        //    get { return cancelCommand ?? (cancelCommand = new Command(async () => await SendReportAsync())); }
        //}

        public ImageSource MomentImageSource
        {
            get;
            private set;
        }

        public MomentPreviewViewModel(byte[] image)
        {
            MomentImageSource = ImageSource.FromStream(() => new MemoryStream(image));

            this.image = image;
        }

        public async Task SendMomentAsync()
        {
            var user = await AmazonWebService.Instance.GetUserAsync();

            var moment = Moment.Create(user);
            moment.Position = App.Instance.CurrentPosition;
            moment.Address = App.Instance.CurrentAddress;
            await AmazonWebService.Instance.SaveMomentAsync(moment, image);

            user.SentMomentsCount++;
            await AmazonWebService.Instance.SaveUserAsync(user);

            await LocalDataService.Instance.SaveMomentAsync(moment, image);

            await NavigationService.Instance.ShowMomentListAsync();

            DialogService.ShowSuccess("Moment sent!");
        }

        private void CompressImageAsync()
        {

        }
    }
}
