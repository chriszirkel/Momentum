using Momentum.Primitives;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Position = Momentum.Primitives.Position;

namespace Momentum.Platform
{
    public interface ILocation
    {
        string GetNameForCountryCode(string countryCode);

        Task<Address> GetAddressForPositionAsync(Position position);

        Task<Address> GetAddressForPositionAsync(double latitude, double longitude);
    }

    public class Location : ILocation
    {
        private static Location instance;
        private ILocation platform;
        private IGeolocator geolocator;

        public static Location Instance
        {
            get { return instance ?? (instance = new Location()); }
        }

        public Primitives.Position CurrentPosition
        {
            get;
            private set;
        }

        public Address CurrentAddress
        {
            get;
            private set;
        }

        public bool HasGeolocation
        {
            get { return geolocator.IsGeolocationAvailable && geolocator.IsGeolocationEnabled; }
        }

        private Location()
        {
            platform = DependencyService.Get<ILocation>();
            geolocator = CrossGeolocator.Current;
        }

        public async Task<Position> GetPositionAsync()
        {
            geolocator.DesiredAccuracy = 1000;
            var position = await geolocator.GetPositionAsync();

            return new Position(position.Latitude, position.Longitude);
        }

        //public async Task<bool> StartGeolocator()
        //{
        //    var locator = CrossGeolocator.Current;
        //    locator.DesiredAccuracy = 1000;

        //    if (locator.IsGeolocationAvailable && locator.IsGeolocationEnabled)
        //    {
        //        locator.PositionChanged += OnPositionChanged;
        //        locator.PositionError += OnPositionError;
        //        return await locator.StartListeningAsync(100, 100);
        //    }
        //    else
        //    {
        //        return await Task.FromResult(false);
        //    }
        //}

        public Task<Address> GetAddressForPositionAsync(Position position)
        {
            return platform.GetAddressForPositionAsync(position);
        }

        public Task<Address> GetAddressForPositionAsync(double latitude, double longitude)
        {
            return platform.GetAddressForPositionAsync(latitude, longitude);
        }

        public string GetNameForCountryCode(string countryCode)
        {
            return platform.GetNameForCountryCode(countryCode);
        }
    }
}
