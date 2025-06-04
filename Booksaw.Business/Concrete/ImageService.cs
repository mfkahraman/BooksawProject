using Booksaw.Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Booksaw.Business.Concrete
{
    public class ImageService : IImageService
    {
        private readonly IHostEnvironment _env;

        // Constructor injection for IHostEnvironment
        public ImageService(IHostEnvironment env)
        {
            _env = env;
        }

        // Save Image method
        public async Task<string> SaveImageAsync(IFormFile file, string subFolder)
        {
            if (file == null || file.Length == 0)
                return null;

            var extension = Path.GetExtension(file.FileName);
            var newFileName = Guid.NewGuid() + extension;

            // Use the IHostEnvironment to get the WebRootPath
            var absolutePath = Path.Combine(_env.ContentRootPath, "wwwroot", "images", subFolder, newFileName);

            // Ensure directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(absolutePath)!);

            using (var stream = new FileStream(absolutePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine("/images", subFolder, newFileName).Replace("\\", "/");
        }

        // Delete Image method
        public async Task<bool> DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return false;

            // Normalize path
            var relativePath = imageUrl.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString());
            var fullPath = Path.Combine(_env.ContentRootPath, "wwwroot", relativePath);

            if (File.Exists(fullPath))
            {
                await Task.Run(() => File.Delete(fullPath));
                return true;
            }

            return false;
        }
    }
}
