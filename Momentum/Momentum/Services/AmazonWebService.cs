using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Momentum.Extensions;
using Momentum.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Momentum.Services
{
    public class AmazonWebService
    {
        private static AmazonWebService instance;

        public static AmazonWebService Instance
        {
            get { return instance ?? (instance = new AmazonWebService()); }
        }

        public CognitoAWSCredentials Credentials
        {
            get;
            private set;
        }

        public AmazonS3Client S3Client
        {
            get;
            private set;
        }

        public TransferUtility TransferUtility
        {
            get;
            set;
        }

        public AmazonDynamoDBClient DBClient
        {
            get;
            private set;
        }

        public DynamoDBContext DBContext
        {
            get;
            private set;
        }

        private AmazonWebService()
        {
            AWSConfigs.LoggingConfig.LogMetrics = true;
            AWSConfigs.LoggingConfig.LogResponses = ResponseLoggingOption.Always;
            AWSConfigs.LoggingConfig.LogMetricsFormat = LogMetricsFormatOption.JSON;
            AWSConfigs.LoggingConfig.LogTo = LoggingOptions.SystemDiagnostics;

            Credentials = new CognitoAWSCredentials(Constants.CognitoIdentityPoolId, Constants.CognitoRegion);

            S3Client = new AmazonS3Client(Credentials, Constants.S3BucketRegion);
            TransferUtility = new TransferUtility(S3Client);

            DBClient = new AmazonDynamoDBClient(Credentials, Constants.DynamoDBRegion);
            DBContext = new DynamoDBContext(DBClient);
        }

        public void AddFacebookLogin(string accessToken)
        {
            Credentials.AddLogin("graph.facebook.com", accessToken);
        }

        public void RemoveFacebookLogin()
        {
            Credentials.RemoveLogin("graph.facebook.com");
        }

        public async Task<IEnumerable<Moment>> GetLatestMomentsAsync()
        {
            List<Moment> moments = new List<Moment>();

            var filter = new QueryFilter();
            filter.AddCondition("date", QueryOperator.LessThanOrEqual, DateTime.UtcNow.ToDateString());

            var query = DBContext.FromQueryAsync<Moment>(new QueryOperationConfig()
            {
                IndexName = "date-time-index",
                Filter = filter,
                BackwardSearch = true,
            });

            var result = await query.GetRemainingAsync();
            moments.AddRange(result);

            //filter = new QueryFilter();
            //filter.AddCondition("date", QueryOperator.Equal, DateTime.UtcNow.AddDays(-1).ToDateString());
            //filter.AddCondition("time", QueryOperator.GreaterThanOrEqual, DateTime.UtcNow.AddDays(-1).ToDateString());

            //query = DBContext.FromQueryAsync<Moment>(new QueryOperationConfig()
            //{
            //    IndexName = "date-time-index",
            //    Filter = filter,
            //    BackwardSearch = true,
            //});

            //result = await query.GetRemainingAsync();
            //moments.AddRange(result);

            return moments;
        }

        public async Task<IEnumerable<Moment>> GetUserMomentsAsync()
        {
            string userId = await GetUserIdAsync();

            return await GetUserMomentsAsync(userId);
        }

        public async Task<IEnumerable<Moment>> GetUserMomentsAsync(string userId)
        {
            var query = DBContext.QueryAsync<Moment>(userId);

            return await query.GetRemainingAsync();
        }

        public async Task<IEnumerable<Thanks>> GetUserThanksAsync()
        {
            string userId = await GetUserIdAsync();

            return await GetUserThanksAsync(userId);
        }

        public async Task<IEnumerable<Thanks>> GetUserThanksAsync(string userId)
        {
            var query = DBContext.QueryAsync<Thanks>(userId);

            return await query.GetRemainingAsync();
        }

        //public async Task<Stream> LoadMomentAsync(Moment moment)
        //{
        //    MemoryStream memStream = new MemoryStream();

        //    using (var stream = await TransferUtility.OpenStreamAsync(Constants.S3BucketName, moment.MomentId))
        //    {
        //        await stream.CopyToAsync(memStream);
        //    }

        //    return memStream;
        //}

        public async Task<string> GetUserIdAsync()
        {
            return await Credentials.GetIdentityIdAsync();
        }

        public async Task<User> GetUserAsync()
        {
            string userId = await GetUserIdAsync();

            return await GetUserAsync(userId);
        }

        public async Task<User> GetUserAsync(string userId)
        {
            return await DBContext.LoadAsync<User>(userId);
        }

        public async Task SaveMomentAsync(Moment moment, byte[] momentImage)
        {
            //var awsMoment = moment.Clone<AwsMoment>();

            await DBContext.SaveAsync(moment);
            await TransferUtility.UploadAsync(new MemoryStream(momentImage), Constants.S3BucketName, moment.MomentId);
        }

        public async Task SaveUserAsync(User user)
        {
            //var awsUser = new AwsUser();
            //awsUser.MoveCorresponding<IUser>(awsUser);

            await DBContext.SaveAsync(user);
        }

        public async Task SaveThanksAsync(Thanks thanks)
        {
            //var awsThanks = new AwsThanks();
            //awsThanks.MoveCorresponding<IThanks>(awsThanks);

            await DBContext.SaveAsync(thanks);
        }

        public async Task SaveReportAsync(Report report)
        {
            //var awsReport = new AwsReport();
            //awsReport.MoveCorresponding<IReport>(awsReport);

            await DBContext.SaveAsync(report);
        }

        public async Task DeleteThanksAsync(Thanks thanks)
        {
            await DBContext.DeleteAsync(thanks);
        }

        public async Task DeleteThanksAsync(IEnumerable<Thanks> thanks)
        {
            var batch = DBContext.CreateBatchWrite<Thanks>();
            batch.AddDeleteItems(thanks);

            await batch.ExecuteAsync();
        }

        public string GetMomentUrl(Moment moment)
        {
            return S3Client.GetPreSignedURL(new GetPreSignedUrlRequest()
            {
                BucketName = Constants.S3BucketName,
                Key = moment.MomentId,
                Expires = DateTime.UtcNow.AddHours(1)
            });
        }
    }
}
