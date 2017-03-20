using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momentum.Models.Aws
{
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
