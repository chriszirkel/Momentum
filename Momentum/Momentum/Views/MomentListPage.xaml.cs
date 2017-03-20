using Momentum.Models;
using Momentum.ViewModels;

using Xamarin.Forms;

namespace Momentum.Views
{
    public partial class MomentListPage : ContentPage
    {
        private MomentListViewModel ViewModel
        {
            get { return BindingContext as MomentListViewModel; }
        }

        public MomentListPage()
        {
            BindingContext = new MomentListViewModel();

            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            MomentListView.Refreshing += async (s, e) =>
            {
                MomentListView.IsRefreshing = true;
                //ViewModel.UpdateItemsCommand.Execute(null);
                await ViewModel.UpdateMomentsAsync();
                MomentListView.IsRefreshing = false;
            };

            MomentListView.ItemSelected += (s, e) =>
            {
                MomentListView.SelectedItem = null;
            };

            MomentListView.ItemTapped += (s, e) =>
            {
                var moment = e.Item as Moment;

                if (moment == null)
                    return;

                App.Current.MainPage.Navigation.PushModalAsync(new MomentViewPage(moment));
                //ViewModel.DestroyImageCommand.Execute(moment);
                MomentListView.SelectedItem = null;
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel == null || ViewModel.IsBusy || ViewModel.Moments.Count > 0)
            {
                return;
            }

            ViewModel.UpdateMomentsCommand.Execute(null);
        }
    }
}
