using Microsoft.AspNetCore.Http;

namespace Yu_Gi_Oh_website.Services.Contracts
{
    public interface IImageUploadService
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
