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
    public interface IThanks
    {
        string UserId { get; set; }
        string ThanksId { get; set; }
        string MomentId { get; set; }
        Position Position { get; set; }
        DateTime Timestamp { get; set; }
        Address Address { get; set; }
    }

    [DynamoDBTable("momentum-mobilehub-268650841-thanks")]
    public class Thanks: IThanks
    {
        public string UserId { get; set; }
        public string ThanksId { get; set; }
        public string MomentId { get; set; }
        public Position Position { get; set; }
        public DateTime Timestamp { get; set; }
        public Address Address { get; set; }

        public static Thanks Create(IMoment moment)
        {
            return new Thanks()
            {
                ThanksId = Guid.NewGuid().ToString(),
                MomentId = moment.MomentId,
                UserId = moment.UserId,
                Timestamp = DateTime.UtcNow
            };
        }
    }
}
