using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;

namespace Momentum.Helpers
{
    public class AuthStore
    {
        //private static string TokenKeyName = "token";

        //public static void CacheAuthToken(MobileServiceUser user)
        //{
        //    //var account = new Account(user.UserId);
        //    //account.Properties.Add(TokenKeyName, user.MobileServiceAuthenticationToken);
        //    ////GetAccountStore().Save(account, Constants.AppName);

        //    //Debug.WriteLine($"Cached auth token: {user.MobileServiceAuthenticationToken}");
        //}

        //public static MobileServiceUser GetUserFromCache()
        //{
        //    //var account = GetAccountStore().FindAccountsForService(Constants.AppName).FirstOrDefault();

        //    //if (account == null)
        //    //{
        //    //    return null;
        //    //}

        //    //var token = account.Properties[TokenKeyName];
        //    //Debug.WriteLine($"Retrieved token from account store: {token}");

        //    //return new MobileServiceUser(account.Username)
        //    //{
        //    //    MobileServiceAuthenticationToken = token
        //    //};
        //}

        //public static void DeleteTokenCache()
        //{
        //    //var accountStore = GetAccountStore();
        //    //var account = accountStore.FindAccountsForService(Constants.AppName).FirstOrDefault();
        //    //if (account != null)
        //    //{
        //    //    accountStore.Delete(account, Constants.AppName);
        //    //}
        //}

        ////private static AccountStore GetAccountStore()
        ////{
        ////    return DependencyService.Get<IPlatform>().GetAccountStore();
        ////}
    }
}
