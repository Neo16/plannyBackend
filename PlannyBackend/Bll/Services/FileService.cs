using PlannyBackend.Bll.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Extensions.Options;
using PlannyBackend.Common.Configurations;

namespace PlannyBackend.Bll.Services
{
    public class FileService : IFileService
    {
        private readonly AzureBlobConfiguration _blobConfiguration;

        public FileService(IOptions<AzureBlobConfiguration> blobOptions)
        {
            _blobConfiguration = blobOptions.Value;
        }

        public async Task<string> DowloadPlannyPicture(string pictureName)
        {
            if (pictureName != null)
            {
                CloudBlobContainer container = getPictureContainer();
                CloudBlockBlob picBlob = container.GetBlockBlobReference(pictureName);

                using (var ms = new MemoryStream())
                {
                    var requestOptions = new BlobRequestOptions()
                    {
                        MaximumExecutionTime = TimeSpan.FromSeconds(2)
                    };
                    try
                    {
                        if (await picBlob.ExistsAsync(requestOptions, null))
                        {
                            await picBlob.DownloadToStreamAsync(ms);
                            ms.Seek(0, SeekOrigin.Begin);

                            string imageBase64Data = Convert.ToBase64String(ms.ToArray());
                            var pic = string.Format("data:image/png;base64,{0}", imageBase64Data);
                            return pic;
                        }
                        return null;
                    }
                    catch (StorageException e)
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        public async Task<string> UploadPlannyPicture(IFormFile picture)
        {

            if (picture != null)
            {
                var fileName = (Guid.NewGuid()).ToString();
                CloudBlobContainer container = getPictureContainer();

                CloudBlockBlob blob = container.GetBlockBlobReference(fileName);
                await blob.UploadFromStreamAsync(picture.OpenReadStream());
                return fileName;
            }
            return null;
        }

        private CloudBlobContainer getPictureContainer()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_blobConfiguration.ConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference("pictures-container");
        }
    }
}
