using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FileManager
{
    public class FileManager : IFileManager
    {
        private readonly string _uploadPath;

        public FileManager(string uploadPath)
        {
            _uploadPath = uploadPath;
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No file uploaded.");
            }
            var guid = Guid.NewGuid().ToString().Substring(0, 7);

            var filePath = Path.Combine(_uploadPath, file.FileName,guid);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
           
            var fileUrl = $"/uploads/{file.FileName}";

            return fileUrl;
        }
        public Task DeleteFileAsync(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("File name cannot be null or empty.");
            }

            var filePath = Path.Combine(_uploadPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            else
            {
                throw new FileNotFoundException("File not found.", fileName);
            }

            return Task.CompletedTask;
        }
    }
}
