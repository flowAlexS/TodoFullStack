namespace TodoApi.Services.MinioService
{
    public interface IMinioService
    {
        Task<string> UploadFile(IFormFile file);
    }
}
