using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Yu_Gi_Oh_website.Services.Contracts;

namespace Yu_Gi_Oh_website.Services.Implementations
{
	public class CloudinaryImageUploadService : IImageUploadService
	{
		private readonly Cloudinary cloudinary;

		public CloudinaryImageUploadService(Cloudinary cloudinary)
		{
			this.cloudinary = cloudinary;
		}
		public async Task<string> UploadAsync(IFormFile file)
		{
            var resultUrl = string.Empty;
            byte[] finalImage;

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            finalImage = memoryStream.ToArray();

            using var destinationStream = new MemoryStream(finalImage);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, destinationStream),
            };
            var result = await cloudinary.UploadAsync(uploadParams);
            resultUrl = result.Url.AbsoluteUri;

            return resultUrl;
        }
	}
}
