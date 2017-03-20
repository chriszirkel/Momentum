using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Momentum.Primitives;
using Momentum.Services;
using Android.Locations;
using Xamarin.Forms;
using Java.Util;
using Address = Momentum.Primitives.Address;
using Position = Momentum.Primitives.Position;
using Plugin.Geolocator.Abstractions;
using Momentum.Platform;

[assembly: Xamarin.Forms.Dependency(typeof(Momentum.Droid.Platform.DroidLocation))]
namespace Momentum.Droid.Platform
{
    public class DroidLocation : ILocation
    {
        public async Task<Address> GetAddressForPositionAsync(Position position)
        {
            return await GetAddressForPositionAsync(position.Latitude, position.Longitude);
        }

        public async Task<Address> GetAddressForPositionAsync(double latitude, double longitude)
        {
            Geocoder geocoder = new Geocoder(Forms.Context, Locale.English);

            var addresses = await geocoder.GetFromLocationAsync(latitude, longitude, 1);
            var address = addresses.FirstOrDefault();

            if (address == null)
            {
                return new Address
                {
                    City = "Schriesheim",
                    PostalCode = "69198",
                    CountryCode = "DE",
                    CountryName = "Germany"
                };
            }
            else
            {
                return new Address
                {
                    City = address.Locality,
                    PostalCode = address.PostalCode,
                    CountryCode = address.CountryCode,
                    CountryName = address.CountryName
                };
            }

            //return null;
        }

        public string GetNameForCountryCode(string countryCode)
        {
            if (string.IsNullOrEmpty(countryCode))
                return string.Empty;

            Locale locale = new Locale("", countryCode);

            return locale.DisplayCountry;
        }
    }
}