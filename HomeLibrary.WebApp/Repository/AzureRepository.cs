using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace HomeLibrary.WebApp.Repository
{
    public class AzureRepository
    {
        private CloudStorageAccount storageAccount;
        private readonly string container = "HomeLibrary";
        public BlobContainerPublicAccessType PublicAccess { get; set; }
        public AzureRepository()
        {
            storageAccount= CloudStorageAccount.Parse("UseDevelopmentStorage=true;");
            CreateBlobContainer(container);
        }

        private async Task CreateBlobContainer(string containerName)
        {
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName.ToLower());

            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions()
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });
        }

        public async Task<string> AddBlobToStorage(string fileName, byte[] imageBuffer = null, Stream stream = null)
        {

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer selectedContainer = blobClient.GetContainerReference(container.ToLower());
            CloudBlockBlob blob = selectedContainer.GetBlockBlobReference(fileName);
            if (imageBuffer != null)
            {
                await blob.UploadFromByteArrayAsync(imageBuffer, 0, imageBuffer.Length);
                return blob.Uri.ToString();
            }
            else if (stream != null)
            {
                try
                {
                    await blob.UploadFromStreamAsync(stream);
                    return blob.Uri.ToString();
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }

    }
}
