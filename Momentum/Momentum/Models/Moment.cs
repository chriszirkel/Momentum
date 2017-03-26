using Amazon.DynamoDBv2.DataModel;
using Momentum.Converters;
using Momentum.Extensions;
using Momentum.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momentum.Models
{
    public interface IMoment
    {
        string UserId { get; set; }
        string MomentId { get; set; }
        DateTime Timestamp { get; set; }
        Position Position { get; set; }
        Address Address { get; set; }
    }

    [DynamoDBTable("momentum-mobilehub-268650841-moments")]
    public class Moment : IMoment
    {
        [DynamoDBHashKey("userId")]
        public string UserId { get; set; }

        [DynamoDBRangeKey("momentId")]
        public string MomentId { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey]
        [DynamoDBProperty("date")]
        public string Date { get; set; }

        [DynamoDBGlobalSecondaryIndexRangeKey]
        [DynamoDBProperty("time")]
        public string Time { get; set; }

        [DynamoDBProperty("position", typeof(PositionPropertyConverter))]
        public Position Position { get; set; }

        [DynamoDBProperty("city")]
        public string City { get; set; }

        [DynamoDBProperty("postalCode")]
        public string PostalCode { get; set; }

        [DynamoDBProperty("countryCode")]
        public string CountryCode { get; set; }

        [DynamoDBProperty("countryName")]
        public string CountryName { get; set; }

        [DynamoDBIgnore]
        public DateTime Timestamp
        {
            get
            {
                return DateTime.ParseExact(string.Format("{0}T{1}Z", Date, Time), "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
            }
            set
            {
                Date = value.ToDateString();
                Time = value.ToTimeString();
            }
        }

        [DynamoDBIgnore]
        public Address Address
        {
            get
            {
                return new Address { City = City, PostalCode = PostalCode, CountryName = CountryName, CountryCode = CountryCode };
            }
            set
            {
                City = value?.City;
                PostalCode = value?.PostalCode;
                CountryName = value?.CountryName;
                CountryCode = value?.CountryCode;
            }
        }

        public static Moment Create(User user)
        {
            return new Moment
            {
                MomentId = Guid.NewGuid().ToString(),
                UserId = user.UserId,
                Timestamp = DateTime.UtcNow
            };
        }
    }
}
