using Microsoft.WindowsAzure.MobileServices;
using Momentum.Helpers;
using Momentum.Services;
using Momentum.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Momentum.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private Command logoutCommand;

        public Command LogoutCommand
        {
            get { return logoutCommand ?? (logoutCommand = new Command(async () => await ExecuteLogoutCommand())); }
        }

        private async Task ExecuteLogoutCommand()
        {
            //if (IsBusy)
            //{
            //    return;
            //}

            //IsBusy = true;

            //await AuthenticationService.Instance.LogoutAsync();

            // https://jfarrell.net/2015/01/22/understanding-xamarin-forms-navigation/
            var navigation = App.Current.MainPage.Navigation;
            navigation.InsertPageBefore(new SignInPage(), navigation.NavigationStack[0]);
            await navigation.PopToRootAsync();

            //IsBusy = false;
        }
    }
}
