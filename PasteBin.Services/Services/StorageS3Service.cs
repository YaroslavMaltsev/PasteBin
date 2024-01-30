using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using PasteBin.Services.Interfaces;
using System.Net;

namespace PasteBin.Services.Services
{
    public class StorageS3Service : IStorageS3Service
    {
        private readonly IConfiguration _configuration;

        public StorageS3Service(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> UploadTextToStorage(string textPasteBin, string key)
        {
            var client = CreateAmazonS3Client();

            var request = new PutObjectRequest
            {
                BucketName = "string", //TODO: Add a new bucket
                Key = key,
                ContentBody = textPasteBin
            };

            var response = await client.PutObjectAsync(request);

            if (response.HttpStatusCode == HttpStatusCode.OK)
                return true;
            else
                return false;
        }
        private AmazonS3Client CreateAmazonS3Client()
        {
            var credentials = new Amazon.Runtime.BasicAWSCredentials(_configuration["AccessKey"], _configuration["SecretKey"]);
            AmazonS3Config config = new AmazonS3Config
            {
                ServiceURL = "https://message-queue.api.cloud.yandex.net",
                AuthenticationRegion = "ru-central1",


            };
            return new AmazonS3Client(credentials, config);
        }
    }
}
