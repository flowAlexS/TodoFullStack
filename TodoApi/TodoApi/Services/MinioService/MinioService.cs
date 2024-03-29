using Minio;

namespace TodoApi.Services.MinioService
{
    public class MinioService
    {
        private const string BUCKETNAME = "";
        private readonly IMinioClient _minioClient;

        public MinioService(IMinioClient minioClient)
        => this._minioClient = minioClient;

        // Upload // Get
    }
}
