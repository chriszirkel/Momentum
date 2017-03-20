using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momentum.Models
{
    public interface IUser
    {
        string UserId { get; set; }
        int SentMomentsCount { get; set; }
        int ReceivedMomentsCount { get; set; }
        int SentThanksCount { get; set; }
        int ReceivedThanksCount { get; set; }
    }

    public class User : IUser
    {
        public string UserId { get; set; }
        public int SentMomentsCount { get; set; }
        public int ReceivedMomentsCount { get; set; }
        public int SentThanksCount { get; set; }
        public int ReceivedThanksCount { get; set; }
    }
}
