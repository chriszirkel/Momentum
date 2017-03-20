using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Momentum.Controls
{
    public class MomentImageCell : ViewCell
    {
        private Image image;

        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create("Source", typeof(ImageSource), typeof(MomentImageCell), null);

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly BindableProperty WidthRequestProperty =
            BindableProperty.Create("WidthRequest", typeof(double), typeof(MomentImageCell), 0.0);

        public double WidthRequest
        {
            get { return (double)GetValue(WidthRequestProperty); }
            set { SetValue(WidthRequestProperty, value); }
        }

        public static readonly BindableProperty HeightRequestProperty =
            BindableProperty.Create("HeightRequest", typeof(double), typeof(MomentImageCell), 0.0);

        public double HeightRequest
        {
            get { return (double)GetValue(HeightRequestProperty); }
            set { SetValue(HeightRequestProperty, value); }
        }

        public MomentImageCell()
        {
            image = new Image() { Aspect = Aspect.AspectFill };

            View = image;
        }

        //public MomentImageCell(int width, int height)
        //{
        //    image = new Image { WidthRequest = width, HeightRequest = height };
        //    //image.SetBinding(Image.SourceProperty, "Source");

        //    View = image;
        //}

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                image.WidthRequest = WidthRequest;
                image.HeightRequest = HeightRequest;
                image.Source = Source;
            }
        }
    }
}
