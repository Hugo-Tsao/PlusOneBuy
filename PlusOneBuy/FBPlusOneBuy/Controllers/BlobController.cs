using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace FBPlusOneBuy.Controllers
{
    public class BlobController : Controller
    {
        private readonly string _storageConnectionString = ConfigurationManager.AppSettings["CONNECT_STR"];

        // GET: Blob
        public async Task<ActionResult> Index(string LivePageID,string JsonContent)
        {
            CloudStorageAccount storageAccount;
            if (CloudStorageAccount.TryParse(_storageConnectionString, out storageAccount))
            {
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                // Create a container called 'quickstartblobs' and 
                // append a GUID value to it to make the name unique.
                CloudBlobContainer cloudBlobContainer =
                    cloudBlobClient.GetContainerReference("fbchart");
                if (!cloudBlobContainer.Exists())
                {
                    await cloudBlobContainer.CreateAsync();
                }
                // Create a file in your local MyDocuments folder to upload to a blob.
                string localPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string localFileName = LivePageID + ".txt";
                string sourceFile = Path.Combine(localPath, localFileName);
                // Write text to the file.
                System.IO.File.WriteAllText(sourceFile, JsonContent);

                // Get a reference to the blob address, then upload the file to the blob.
                // Use the value of localFileName for the blob name.
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(localFileName);
                await cloudBlockBlob.UploadFromFileAsync(sourceFile);
                System.IO.File.Delete(sourceFile);
            }
            else
            {
                // Otherwise, let the user know that they need to define the environment variable.
                throw new Exception(
                    $"A connection string has not been defined in the system environment variables.Add an environment variable named 'CONNECT_STR' with your storageconnection string as a value.");
            }
            
            return View();
        }
    }
}