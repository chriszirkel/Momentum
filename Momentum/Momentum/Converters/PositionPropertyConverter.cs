using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Momentum.Primitives;
//using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Xamarin.Forms.Maps;

namespace Momentum.Converters
{
    public class PositionPropertyConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            Position position = (Position)value;

            if (position == null)
                throw new ArgumentOutOfRangeException();

            DynamoDBEntry entry = new Primitive { Value = string.Format("{0},{1}", position.Latitude, position.Longitude) };

            return entry;
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            Primitive primitive = entry as Primitive;

            if (primitive == null || !(primitive.Value is string) || string.IsNullOrEmpty((string)primitive.Value))
                throw new ArgumentOutOfRangeException();

            string[] position = ((string)primitive.Value).Split(',');

            if (position.Length != 2)
                throw new ArgumentOutOfRangeException();

            double lat = Convert.ToDouble(position[0]);
            double lng = Convert.ToDouble(position[1]);

            return new Position(lat, lng);
        }
    }
}
