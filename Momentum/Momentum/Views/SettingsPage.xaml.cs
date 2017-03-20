using Momentum.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Momentum.Views
{
    public partial class SettingsPage : ContentPage
    {
        private SettingsViewModel ViewModel
        {
            get { return BindingContext as SettingsViewModel; }
        }

        public SettingsPage()
        {
            BindingContext = new SettingsViewModel();

            InitializeComponent();
        }
    }
}
