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
    public partial class SignInPage : ContentPage
    {
        private SignInViewModel ViewModel
        {
            get { return BindingContext as SignInViewModel; }
        }

        public SignInPage()
        {
            BindingContext = new SignInViewModel();

            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            facebookLoginButton.LoginSuccess += async (sender, e) =>
            {
                await ViewModel.OnFacebookLoginSuccess(e.AccessToken);
            };

            facebookLoginButton.LoginError += (sender, e) =>
            {
                ViewModel.OnFacebookLoginError(e.Message);
            };
        }
    }
}
