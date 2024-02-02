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
                BucketName = "pastebintestproject",
                Key = $"{key}.txt",
                ContentBody = textPasteBin
            };

            var response = await client.PutObjectAsync(request);

            client.Dispose();

            switch (response.HttpStatusCode)
            {
                case  HttpStatusCode.OK:
                    return true;
                default:
                    return false;
                    
            }
        }
        public async Task<string> GetTextPasteToS3(string key)
        {
            var client = CreateAmazonS3Client();

            var response = await client.GetObjectAsync("pastebintestproject", $"{key}.txt");
           
            client.Dispose();
            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                using (var reader = new StreamReader(response.ResponseStream))
                {
                    string text = reader.ReadToEnd();
                    return text;
                }
            }
            return null;
        }
        private AmazonS3Client CreateAmazonS3Client()
        {
            AmazonS3Config config = new AmazonS3Config
            {
                ServiceURL = "https://s3.yandexcloud.net",
            };
            return new AmazonS3Client(_configuration["AWS:AccessKey"], _configuration["AWS:SecretKey"],config);
        }
    }
}
