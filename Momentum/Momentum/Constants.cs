using Amazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momentum
{
    public static class Constants
    {
        public static readonly string AppName = "Momentum";
        public static readonly string ApplicationUrl = "https://momentumapp.azurewebsites.net";
        public static readonly string ApplicationHost = "momentumapp.azurewebsites.net";
        public static readonly string ContainerName = "images";
        public static readonly string ContainerUrl = "https://ma60c61fbe15764b.blob.core.windows.net/images";
        public static readonly string AzureStorageAccountConnectionString = "DefaultEndpointsProtocol=https;AccountName=ma60c61fbe15764b;AccountKey=urnL5NG87jzoJ5wi4xYZaLwqMRJFRrMO99iVlrTgeDdtfSgX99u5FojvwMMn8a+jOG35QtOiuSuQHK4qDRsqjQ==";

        public static readonly string Host = "google.com";
        public static readonly RegionEndpoint CognitoRegion = RegionEndpoint.USEast1;
        public static readonly string CognitoIdentityPoolId = "us-east-1:9cdfad84-973a-4524-84c9-632e721ec6d9";
        public static readonly RegionEndpoint S3BucketRegion = RegionEndpoint.USEast1;
        public static readonly string S3BucketName = "momentum-contentdelivery-mobilehub-268650841";
        public static readonly RegionEndpoint DynamoDBRegion = RegionEndpoint.USEast1;
    }
}
