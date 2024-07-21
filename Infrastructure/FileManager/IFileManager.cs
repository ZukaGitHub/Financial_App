using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FileManager
{
    public interface IFileManager
    {
        Task<string> SaveFileAsync(IFormFile file);
        Task DeleteFileAsync(string fileName);
    }
}
