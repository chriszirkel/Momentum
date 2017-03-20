using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momentum.Converters
{
    public class TimePropertyConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            TimeSpan time = (TimeSpan) value;

            if (time == null)
                throw new ArgumentOutOfRangeException();

            DynamoDBEntry entry = new Primitive { Value = time.ToString("HH:mm:ss") };

            return entry;
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            Primitive primitive = entry as Primitive;

            if (primitive == null || !(primitive.Value is string) || string.IsNullOrEmpty((string)primitive.Value))
                throw new ArgumentOutOfRangeException();

            TimeSpan time = TimeSpan.ParseExact((string) primitive.Value, "HH:mm:ss", CultureInfo.InvariantCulture);

            return time;
        }
    }
}
