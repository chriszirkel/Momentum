using Amazon.DynamoDBv2.DataModel;
using Momentum.Converters;
using Momentum.Extensions;
using Momentum.Primitives;
using SQLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momentum.Models.Sql
{
    [Table("sentMoments")]
    public class SentMoment : IMoment
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [Column("userId")]
        public string UserId { get; set; }

        [Column("momentId")]
        public string MomentId { get; set; }

        [Column("date")]
        public string Date { get; set; }

        [Column("time")]
        public string Time { get; set; }

        [Column("latitude")]
        public double Latitude { get; set; }

        [Column("longitude")]
        public double Longitude { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("postalCode")]
        public string PostalCode { get; set; }

        [Column("countryCode")]
        public string CountryCode { get; set; }

        [Column("countryName")]
        public string CountryName { get; set; }

        [Ignore]
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

        [Ignore]
        public Position Position
        {
            get
            {
                return new Position(Latitude, Longitude);
            }
            set
            {
                Latitude = value.Latitude;
                Longitude = value.Longitude;
            }
        }

        [Ignore]
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

        [Ignore]
        public string ImageSource { get; set; }
    }
}
