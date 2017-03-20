using Momentum.Models;
using Momentum.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Momentum.Views
{
    public partial class MomentPreviewPage : ContentPage
    {
        public MomentPreviewPage(byte[] image)
        {
            BindingContext = new MomentPreviewViewModel(image);

            InitializeComponent();
            SetupEventHandlers();
        }

        private MomentPreviewViewModel ViewModel
        {
            get { return BindingContext as MomentPreviewViewModel; }
        }

        private void SetupEventHandlers()
        {
            var cancelButtonTapped = new TapGestureRecognizer();
            cancelButtonTapped.Tapped += async (object sender, EventArgs e) =>
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
            };
            CancelButton.GestureRecognizers.Add(cancelButtonTapped);

            //var sendButtonTapped = new TapGestureRecognizer();
            //sendButtonTapped.Tapped += async (object sender, EventArgs e) =>
            //{
            //    await ViewModel.SendMomentAsync();
            //};
            //SendButton.GestureRecognizers.Add(sendButtonTapped);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //ViewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            //ViewModel.OnDisappearing();
        }
    }
}
