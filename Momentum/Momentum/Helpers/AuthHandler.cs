﻿using Microsoft.WindowsAzure.MobileServices;
using Momentum.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Momentum.Helpers
{
    public class AuthHandler : DelegatingHandler
    {
        //public IMobileServiceClient Client { get; set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //if (this.Client == null)
            //{
            //    throw new InvalidOperationException("Make sure to set the 'Client' property in this handler before using it.");
            //}

            // Cloning the request, in case we need to send it again
            var clonedRequest = await CloneRequestAsync(request);
            var response = await base.SendAsync(clonedRequest, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                try
                {
                    //AuthStore.DeleteTokenCache(); // cached token was invalid, so should clear it
                    //await DoLoginAsync(Settings.Current.AuthenticationType);
                    //await DoLoginAsync(MobileServiceAuthenticationProvider.Facebook);
                    //await AuthenticationService.Instance.LoginAsync(Settings.AuthenticationProvider);

                    clonedRequest = await CloneRequestAsync(request);

                    clonedRequest.Headers.Remove("X-ZUMO-AUTH");
                    //clonedRequest.Headers.Add("X-ZUMO-AUTH", Client.CurrentUser.MobileServiceAuthenticationToken);
                    clonedRequest.Headers.Add("X-ZUMO-AUTH", AzureService.Instance.Client.CurrentUser.MobileServiceAuthenticationToken);

                    // Resend the request
                    response = await base.SendAsync(clonedRequest, cancellationToken);
                }
                catch (InvalidOperationException)
                {
                    // user cancelled auth, so return the original response
                    return response;
                }
            }

            return response;
        }

        //public static async Task<MobileServiceUser> DoLoginAsync(MobileServiceAuthenticationProvider authOption)
        //{
        //    //if (authOption == Settings.AuthOption.GuestAccess)
        //    //{
        //    //    Settings.Current.CurrentUserId = Settings.Current.DefaultUserId;
        //    //    return; // can't authenticate
        //    //}

        //    var mobileClient = DependencyService.Get<IPlatform>();

        //    var user =
        //        authOption == MobileServiceAuthenticationProvider.Facebook ?
        //            await mobileClient.LoginFacebookAsync() :
        //            await mobileClient.LoginAsync(MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory);

        //    //App.Instance.AuthenticatedUser = user;
        //    System.Diagnostics.Debug.WriteLine("Authenticated with user: " + user.UserId);

        //    //Settings.Current.CurrentUserId =
        //    //    await App.Instance.MobileService.InvokeApiAsync<string>(
        //    //    "ManageUser",
        //    //    System.Net.Http.HttpMethod.Get,
        //    //    null);

        //    //Debug.WriteLine($"Set current userID to: {Settings.Current.CurrentUserId}");

        //    AuthStore.CacheAuthToken(user);

        //    return user;
        //}

        private async Task<HttpRequestMessage> CloneRequestAsync(HttpRequestMessage request)
        {
            var result = new HttpRequestMessage(request.Method, request.RequestUri);
            foreach (var header in request.Headers)
            {
                result.Headers.Add(header.Key, header.Value);
            }

            if (request.Content != null && request.Content.Headers.ContentType != null)
            {
                var requestBody = await request.Content.ReadAsStringAsync();
                var mediaType = request.Content.Headers.ContentType.MediaType;
                result.Content = new StringContent(requestBody, Encoding.UTF8, mediaType);
                foreach (var header in request.Content.Headers)
                {
                    if (!header.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
                    {
                        result.Content.Headers.Add(header.Key, header.Value);
                    }
                }
            }

            return result;
        }
    }
}
