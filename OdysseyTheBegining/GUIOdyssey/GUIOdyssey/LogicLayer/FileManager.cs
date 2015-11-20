using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GUIOdyssey.LogicLayer
{
    class FileManager
    {


        public string uploadFile(string path)
        {
            string currentUserID = SessionManager.Instance.UserId.ToString();
            
            string fileName = Path.GetFileName(path);
            string combinedPath = Path.Combine(currentUserID, fileName);
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["StorageConnectionString"]);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("odysseymusic");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(combinedPath);

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(path))
            {
                blockBlob.UploadFromStream(fileStream);
            }
            return blockBlob.Uri.ToString();
        }

        public string downloadFile(string link)
        {
            Uri uri = new Uri(link);
            string userId = SessionManager.Instance.UserId.ToString();
            var filename = System.IO.Path.GetFileName(uri.LocalPath);
            string userOdysseyMusicPath = GetUserPathToOdysseyMusic();
            string detinationPath = Path.Combine(userOdysseyMusicPath, filename);
            string BlobFileName = Path.Combine(userId, filename);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
               ConfigurationManager.AppSettings["StorageConnectionString"]);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("odysseymusic");

            
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(BlobFileName);

            // Save blob contents to a file.
            using (var fileStream = System.IO.File.OpenWrite(detinationPath))
            {
                blockBlob.DownloadToStream(fileStream);
            }
            return detinationPath;
        }


        public void CreateUserDirectory()
        {
            string pathToUserOdysseyMusic = GetUserPathToOdysseyMusic();
            System.IO.Directory.CreateDirectory(pathToUserOdysseyMusic);
        }

        public string GetUserPathToOdysseyMusic()
        {
            String pathToDoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string currentUserNickname = SessionManager.Instance.Nickname;
            string odysseyDirectory = "OdysseyMusic";
            String combinedOdysseyUserPath = Path.Combine(pathToDoc, odysseyDirectory);
            String finalPath = Path.Combine(combinedOdysseyUserPath, currentUserNickname);
            return finalPath;
        }
    }
}
