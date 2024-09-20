using Amazon.S3.Model;
using Amazon.S3;

namespace StorageMicroservice.Services
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3Service(IAmazonS3 s3Client, string bucketName)
        {
            _s3Client = s3Client;
            _bucketName = bucketName;
        }

        // Upload file method
        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName,
                InputStream = fileStream,
                ContentType = "application/octet-stream", 
                AutoCloseStream = true,
                CannedACL = S3CannedACL.PublicRead 
            };

            await _s3Client.PutObjectAsync(putRequest);
            return $"https://{_bucketName}.s3.amazonaws.com/{fileName}";
        }

        // Delete file method
        public async Task DeleteFileAsync(string fileName)
        {
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName
            };

            await _s3Client.DeleteObjectAsync(deleteRequest);
        }
    }
}
