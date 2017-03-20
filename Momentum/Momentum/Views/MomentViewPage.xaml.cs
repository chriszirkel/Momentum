using Momentum.Models;
using Momentum.ViewModels;

using Xamarin.Forms;

namespace Momentum.Views
{
    public partial class MomentViewPage : ContentPage
    {
        private MomentViewViewModel ViewModel
        {
            get { return BindingContext as MomentViewViewModel; }
        }

        public MomentViewPage(Moment moment)
        {
            BindingContext = new MomentViewViewModel(moment);

            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            MomentImage.PropertyChanged += (sender, args) =>
            {
                var image = (Image)sender;

                if (image.Source == null)
                    return;

                if (args.PropertyName == nameof(image.IsLoading))
                {
                    ViewModel.IsBusy = image.IsLoading;
                    ButtonOverlay.IsVisible = !image.IsLoading;
                }
            };

           // var reportButtonTapped = new TapGestureRecognizer();
           // reportButtonTapped.Tapped += async (object sender, EventArgs e) =>
           //    {
           //        await ViewModel.OnReportClicked();
           //    };
           // ReportButton.GestureRecognizers.Add(reportButtonTapped);

           // var thankButtonTapped = new TapGestureRecognizer();
           // thankButtonTapped.Tapped += async (object sender, EventArgs e) =>
           //{
           //    await ViewModel.OnThanksClicked();
           //};
           // ThankButton.GestureRecognizers.Add(thankButtonTapped);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await ViewModel.InitializeAsync();
        }
    }
}
