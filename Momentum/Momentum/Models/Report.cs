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
    public interface IReport
    {
        string UserId { get; set; }
        string ReportId { get; set; }
        string MomentId { get; set; }
        Position Position { get; set; }
        DateTime Timestamp { get; set; }
        Address Address { get; set; }
    }

    public class Report : IReport
    {
        public string UserId { get; set; }
        public string ReportId { get; set; }
        public string MomentId { get; set; }
        public Position Position { get; set; }
        public DateTime Timestamp { get; set; }
        public Address Address { get; set; }
    }
}
