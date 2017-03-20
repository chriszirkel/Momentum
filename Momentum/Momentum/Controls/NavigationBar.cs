using Momentum.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Momentum.Controls
{
    public class NavigationBar : Grid
    {
        private Image Button;

        public NavigationBar()
        {
            SetupUserInterface();
            SetupEventHandlers();
        }

        private void SetupUserInterface()
        {
            BackgroundColor = Color.Black; //Color.FromHex("#FEFEFE");
            Padding = new Thickness(5);

            var horizontalStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            var verticalStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            var label = new Label
            {
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = Color.White,
                Text = "Momentum"
            };

            var buttonImage = new Image
            {
                Source = "ic_notifications_white_24dp.png"
            };

            var buttonBackground = new BoxView
            {

            };

            var buttonGrid = new Grid
            {
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            var seperator = new BoxView
            {
                Color = Color.FromHex("#EBEBEB"),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 1,
            };

            //buttonGrid.Children.Add(buttonBackground);
            buttonGrid.Children.Add(buttonImage);

            horizontalStack.Children.Add(label);
            horizontalStack.Children.Add(buttonGrid);

            verticalStack.Children.Add(horizontalStack);
            //verticalStack.Children.Add(seperator);

            Children.Add(verticalStack);

            Button = buttonImage;
        }

        private void SetupEventHandlers()
        {
            var buttonTap = new TapGestureRecognizer();
            buttonTap.Tapped += async (object sender, EventArgs e) =>
            {
                await NavigationService.Instance.ShowNotificationsAsync();
            };
            Button.GestureRecognizers.Add(buttonTap);
        }
    }
}
