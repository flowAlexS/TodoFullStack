using Minio;
using Minio.DataModel.Args;

namespace TodoApi.Services.MinioService
{
    public class MinioService : IMinioService
    {
        private const string BUCKETNAME = "todoapp";
        private readonly IMinioClient _minioClient;

        public MinioService(IMinioClient minioClient)
        => this._minioClient = minioClient;

        public async Task<string> UploadFile(IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    var objectName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                    var args = new PutObjectArgs()
                        .WithBucket(BUCKETNAME)
                        .WithObject(objectName)
                        .WithStreamData(file.OpenReadStream())
                        .WithObjectSize(file.Length)
                        .WithContentType(file.ContentType);

                    await _minioClient.PutObjectAsync(args);

                    return objectName;
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
    }

    public static class MinioServiceStatic
    {
        public static async Task<string> UploadFile(IMinioClient client, IFormFile file)
        {
            var bucketName = "testingbucket";
            var objectName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            var args = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithStreamData(file.OpenReadStream())
                .WithObjectSize(file.Length)
                .WithContentType(file.ContentType);

            await client.PutObjectAsync(args);

            return objectName;
        }
    }
}
