using Momentum.Models;
using Momentum.Services;
using System.Threading.Tasks;

namespace Momentum.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        //string username;
        //string password;

        //private Command loginGoogleCommand;
        //private Command loginTwitterCommand;
        //private Command loginFacebookCommand;
        //private Command login2FacebookCommand;

        //public string Username
        //{
        //    get { return username; }
        //    set { username = value; OnPropertyChanged("Username"); }
        //}

        //public string Password
        //{
        //    get { return password; }
        //    set { password = value; OnPropertyChanged("Password"); }
        //}

        //public Command LoginGoogleCommand
        //{
        //    get { return loginGoogleCommand ?? (loginGoogleCommand = new Command(async () => await ExecuteLoginCommand(MobileServiceAuthenticationProvider.Google))); }
        //}

        //public Command LoginTwitterCommand
        //{
        //    get { return loginTwitterCommand ?? (loginTwitterCommand = new Command(async () => await ExecuteLoginCommand(MobileServiceAuthenticationProvider.Twitter))); }
        //}

        //public Command LoginFacebookCommand
        //{
        //    get { return loginFacebookCommand ?? (loginFacebookCommand = new Command(async () => await ExecuteLoginCommand(MobileServiceAuthenticationProvider.Facebook))); }
        //}

        //public Command Login2FacebookCommand
        //{
        //    get { return login2FacebookCommand ?? (login2FacebookCommand = new Command(async () => await ExecuteLoginCommand(MobileServiceAuthenticationProvider.Facebook))); }
        //}

        public async Task OnFacebookLoginSuccess(string accessToken)
        {
            //await AuthenticationService.Instance.LoginFacebookAsync(accessToken);

            AmazonWebService.Instance.AddFacebookLogin(accessToken);

            string userId = await AmazonWebService.Instance.GetUserIdAsync();

            if (await AmazonWebService.Instance.GetUserAsync(userId) == null)
            {
                var user = new User();
                user.UserId = userId;
                await AmazonWebService.Instance.SaveUserAsync(user);
            }

            App.Instance.NavigateToMainUI();
        }

        public void OnFacebookLoginError(string message)
        {
            DialogService.ShowError(message);
        }

        //private async Task ExecuteLoginCommand(MobileServiceAuthenticationProvider provider)
        //{
        //    //if (IsBusy)
        //    //{
        //    //    return;
        //    //}

        //    //IsBusy = true;

        //    //try
        //    //{
        //    //    //DialogService.ShowLoading("Sign in");

        //    //    if (await ConnectivityService.IsConnected())
        //    //    {
        //    //        var result = await AuthenticationService.Instance.LoginAsync(provider);

        //    //        if (result != null)
        //    //        {
        //    //            NavigateToMainUI();
        //    //        }
        //    //        else
        //    //        {
        //    //            DialogService.ShowError("InvalidCredentials");
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        DialogService.ShowError("NoInternetConnection");
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    //Xamarin.Insights.Report(ex);
        //    //}

        //    //IsBusy = false;
        //}

        //private void NavigateToMainUI()
        //{
        //    App.Current.MainPage = App.FetchMainUI();
        //}
    }
}
