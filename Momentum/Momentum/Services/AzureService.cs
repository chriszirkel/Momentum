using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Momentum.Helpers;
using Momentum.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Momentum.Services
{
    public partial class AzureService
    {
        private static AzureService instance;

        //private IMobileServiceTable<Moment> itemTable;
        //private IMobileServiceTable<Country> countryTable;

        public static AzureService Instance
        {
            get
            {
                return instance ?? (instance = new AzureService());
            }
        }

        public MobileServiceClient Client
        {
            get;
            private set;
        }

        private AzureService()
        {
            Client = new MobileServiceClient(Constants.ApplicationUrl, new LoggingHandler(true), new AuthHandler());

            //itemTable = Client.GetTable<Moment>();
            //countryTable = Client.GetTable<Country>();
        }

        //public async Task<bool> SaveCountryAsync(Country country)
        //{
        //    bool result;

        //    try
        //    {
        //        await countryTable.InsertAsync(country);

        //        result = true;
        //    }
        //    catch
        //    {
        //        result = false;
        //    }

        //    return result;
        //}

        //public async Task<IEnumerable<Country>> GetCountriesAsync()
        //{
        //    var dictionary = new Dictionary<string, string>();

        //    return await Client.InvokeApiAsync<IEnumerable<Country>>("get_countries", HttpMethod.Get, dictionary);
        //}

        //public async Task<IEnumerable<Moment>> GetItemsAsync(Expression<Func<Moment, bool>> predicate = null)
        //{
        //    try
        //    {
        //        IEnumerable<Moment> items = null;

        //        if (predicate == null)
        //        {
        //            items = await itemTable.ToEnumerableAsync();
        //        }
        //        else
        //        {
        //            items = await itemTable.Where(predicate).ToEnumerableAsync();
        //        }

        //        return items;
        //    }
        //    catch (MobileServiceInvalidOperationException msioe)
        //    {
        //        Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(@"Sync error: {0}", e.Message);
        //    }

        //    return null;
        //}

        //public async Task<bool> SaveItemAsync(Moment item, Stream data)
        //{
        //    bool result;

        //    string sas = await FetchSasAsync();

        //    try
        //    {
        //        var blob = await SaveBlobAsync(data);
        //        //item.Url = blob.Uri.ToString();

        //        await itemTable.InsertAsync(item);

        //        result = true;
        //    }
        //    catch
        //    {
        //        result = false;
        //    }

        //    return result;
        //}

        //private async Task<CloudBlockBlob> SaveBlobAsync(Stream data)
        //{
        //    var blobName = Guid.NewGuid().ToString().ToLower();

        //    var sas = await FetchSasAsync();
        //    var credentials = new StorageCredentials(sas);

        //    var container = new CloudBlobContainer(new Uri(Constants.ContainerUrl), credentials);
        //    var blob = container.GetBlockBlobReference(blobName);

        //    await blob.UploadFromStreamAsync(data);

        //    //Xamarin.Insights.Track("ImageUploaded");

        //    return blob;
        //}

        //private async Task<string> FetchSasAsync()
        //{
        //    var dictionary = new Dictionary<string, string>();
        //    dictionary.Add("containerName", Constants.ContainerName);

        //    return await Client.InvokeApiAsync<string>("sas", HttpMethod.Get, dictionary);
        //}
    }
}
