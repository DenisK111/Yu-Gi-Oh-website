using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;

namespace Yu_Gi_Oh_website.Services.Contracts
{
    public interface IImageUploadService
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
