using PlannyBackend.Bll.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;

namespace PlannyBackend.Bll.Services
{
    public class FileService : IFileService
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public FileService(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<string> UploadPicture(IFormFile picture)
        {
            if (picture != null)
            {
                var fileName = (Guid.NewGuid()).ToString();
                await UploadFiles(new List<(IFormFile data, string fileName)>() { (picture, fileName) }, "pictures");   
                return fileName;
            }
            return null;
        }

        public async Task UploadFiles(IList<(IFormFile data, string fileName)> files, string destinationFolderPath)
        {
            var uploads = Path.Combine(hostingEnvironment.WebRootPath, destinationFolderPath);
            foreach (var file in files)
            {
                if (file.data.Length > 0)
                {
                    var filePath = Path.Combine(uploads, file.fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.data.CopyToAsync(fileStream);
                    }
                }
            }            
        }
    }
}
