namespace BlobStorage
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Auth;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Microsoft.WindowsAzure.Storage.RetryPolicies;

    public static class GettingStarted
    {
        private const string ContainerPrefix = "infnet-";

        public static void executarComandosBlob()
        {
            Console.WriteLine("Operações no Blob");
            BasicStorageAsync().Wait();
        }

        private static async Task BasicStorageAsync()
        {
            const string ImageToUpload = "tiger.jpg";
            string containerName = ContainerPrefix + Guid.NewGuid();

            CloudStorageAccount storageAccount = Common.CreateStorageAccountFromConnectionString();

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            Console.WriteLine("1. Criando o Container");
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            try
            {
                BlobRequestOptions requestOptions = new BlobRequestOptions() { RetryPolicy = new NoRetry() };
                await container.CreateIfNotExistsAsync(requestOptions, null);
            }
            catch (StorageException)
            {
                Console.WriteLine("Erro ao obter chave de acesso.");
                Console.ReadLine();
                throw;
            }

            Console.WriteLine("2. Uploading Blob");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(ImageToUpload);


            blockBlob.Properties.ContentType = "image/png";
            await blockBlob.UploadFromFileAsync(ImageToUpload);


            Console.WriteLine("3. List Blobs in Container");
            foreach (IListBlobItem blob in container.ListBlobs())
            {
                Console.WriteLine("- {0} (type: {1})", blob.Uri, blob.GetType());
            }

            // Download do blob
            Console.WriteLine("4. Download do Blob {0}", blockBlob.Uri.AbsoluteUri);
            await blockBlob.DownloadToFileAsync(string.Format("./CopyOf{0}", ImageToUpload), FileMode.Create);

            // Cria um snapshot read-only do blob
            //Console.WriteLine("5. Criar snapshot read-only do blob");
            //CloudBlockBlob blockBlobSnapshot = await blockBlob.CreateSnapshotAsync(null, null, null, null);

            //Console.WriteLine("7. Deletar o Container");
            //await container.DeleteIfExistsAsync();
        }
    }
}
