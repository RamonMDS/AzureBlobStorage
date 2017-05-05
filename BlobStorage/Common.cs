namespace BlobStorage
{
    using System;
    using Microsoft.Azure;
    using Microsoft.WindowsAzure.Storage;

    public static class Common
    {
        public static CloudStorageAccount CreateStorageAccountFromConnectionString()
        {
            CloudStorageAccount storageAccount;
            const string Message = "Informação de conta inválida.";

            try
            {
                storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            }
            catch (FormatException)
            {
                Console.WriteLine(Message);
                Console.ReadLine();
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine(Message);
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }    
    }
}
