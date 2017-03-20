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
    public class DatePropertyConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            DateTime date = (DateTime) value;

            if (date == null)
                throw new ArgumentOutOfRangeException();

            DynamoDBEntry entry = new Primitive { Value = date.ToString("yyyy-MM-dd") };

            return entry;
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            Primitive primitive = entry as Primitive;

            if (primitive == null || !(primitive.Value is string) || string.IsNullOrEmpty((string)primitive.Value))
                throw new ArgumentOutOfRangeException();

            DateTime date = DateTime.ParseExact((string) primitive.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            return date;
        }
    }
}
