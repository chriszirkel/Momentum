using Momentum.Controls;
using Momentum.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Momentum.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfileViewModel ViewModel
        {
            get { return BindingContext as ProfileViewModel; }
        }

        public ProfilePage()
        {
            BindingContext = new ProfileViewModel();

            InitializeComponent();
            SetupUserInterface();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await ViewModel.InitializeAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            //ViewModel.OnDisappearing();
        }

        private void SetupUserInterface()
        {
            double itemSize = ((int)App.ScreenWidth / 4) - 10;

            MomentGrid.ItemWidth = itemSize;
            MomentGrid.ItemHeight = itemSize;
            MomentGrid.RowSpacing = 10;
            MomentGrid.ColumnSpacing = 10;

            MomentGrid.ItemTemplate.SetValue(MomentImageCell.WidthRequestProperty, itemSize);
            MomentGrid.ItemTemplate.SetValue(MomentImageCell.HeightRequestProperty, itemSize);

            MomentGrid.ItemSelected += MomentGrid_ItemSelected;

            //MomentGrid.ItemTemplate = new DataTemplate(() => new MomentImageCell(itemSize, itemSize));
            //MomentGrid.ItemTemplate.SetBinding(MomentImageCell.SourceProperty, "Source");
        }

        private void MomentGrid_ItemSelected(object sender, EventArgs<object> e)
        {
            throw new NotImplementedException();
        }

        public void OnDebug(object s, object e)
        {

        }
    }
}
