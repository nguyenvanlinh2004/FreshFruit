using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FreshFruit.Helpers
{
    public static class ImageHelper
    {
        public static async Task<string> SaveAndResizeImageAsync(
            IFormFile image,
            IWebHostEnvironment env,
            string productName,
            string folder = "images",
            int width = 800)
        {
            // Sinh slug từ tên sản phẩm
            string safeFileName = SlugHelper.GenerateSlug(productName);

            // Lấy phần mở rộng của file (vd: .jpg, .png)
            string extension = Path.GetExtension(image.FileName);

            // Tên file dạng: ten-san-pham_guid.ext
            string fileName = $"{safeFileName}_{Guid.NewGuid()}{extension}";
            string folderPath = Path.Combine(env.WebRootPath, folder);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, fileName);

            using var stream = image.OpenReadStream();
            using var img = await SixLabors.ImageSharp.Image.LoadAsync(stream);
            img.Mutate(x => x.Resize(new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(width, 0) // chiều cao tự co theo tỉ lệ
            }));

            await img.SaveAsync(filePath);

            return fileName;
        }

        public static void DeleteImage(IWebHostEnvironment env, string fileName, string folder = "images")
        {
            string path = Path.Combine(env.WebRootPath, folder, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
