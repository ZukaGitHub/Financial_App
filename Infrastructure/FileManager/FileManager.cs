using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
            var fileName=String.Concat(guid,file.FileName);

            var filePath = Path.Combine(_uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }        

            return fileName;
        }
        public Task<bool>DeleteFileAsync(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("File name cannot be null or empty.");
            }
            fileName = fileName.TrimStart('/', '\\');
            if (fileName.StartsWith("uploads", StringComparison.OrdinalIgnoreCase))
            {
                fileName = fileName.Substring("uploads".Length).TrimStart('/', '\\');
            }

            var filePath = Path.Combine(_uploadPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            else
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}
