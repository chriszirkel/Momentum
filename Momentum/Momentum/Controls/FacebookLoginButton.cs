using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Momentum.Controls
{
    public class FacebookLoginButton : Button
    {
        public event EventHandler<SuccessEventArgs> LoginSuccess;
        public event EventHandler<CancelEventArgs> LoginCancel;
        public event EventHandler<ErrorEventArgs> LoginError;

        public void OnLoginSuccess(string accessToken)
        {
            var loginSuccess = LoginSuccess;

            if (loginSuccess != null)
                loginSuccess(this, new SuccessEventArgs() { AccessToken = accessToken });
        }

        public void OnLoginCancel()
        {
            var loginCancel = LoginCancel;

            if (loginCancel != null)
                loginCancel(this, new CancelEventArgs());
        }

        public void OnLoginError(string message)
        {
            var loginError = LoginError;

            if (loginError != null)
                loginError(this, new ErrorEventArgs() { Message = message });
        }

        public class SuccessEventArgs : EventArgs
        {
            public string AccessToken { get; set; }
        }

        public class CancelEventArgs : EventArgs
        {

        }

        public class ErrorEventArgs : EventArgs
        {
            public string Message { get; set; }
        }
    }
}
