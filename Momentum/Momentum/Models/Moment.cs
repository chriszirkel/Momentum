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

    public class Moment : IMoment
    {
        public string UserId { get; set; }
        public string MomentId { get; set; }
        public Position Position { get; set; }
        public DateTime Timestamp { get; set; }
        public Address Address { get; set; }

        public static Moment Create(IUser user)
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
