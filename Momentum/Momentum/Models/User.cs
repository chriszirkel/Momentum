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

    [DynamoDBTable("momentum-mobilehub-268650841-users")]
    public class User : IUser
    {
        [DynamoDBHashKey("userId")]
        public string UserId { get; set; }

        [DynamoDBProperty("sentMomentsCount")]
        public int SentMomentsCount { get; set; }

        [DynamoDBProperty("receivedMomentsCount")]
        public int ReceivedMomentsCount { get; set; }

        [DynamoDBProperty("sentThanksCount")]
        public int SentThanksCount { get; set; }

        [DynamoDBProperty("receivedThanksCount")]
        public int ReceivedThanksCount { get; set; }
    }
}
