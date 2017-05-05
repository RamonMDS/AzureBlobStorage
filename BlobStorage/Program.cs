namespace BlobStorage
{
    using System;

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Azure Blob Storage\n");
            GettingStarted.executarComandosBlob();

            Console.WriteLine();
            Console.WriteLine("Aperte enter para sair.");
            Console.ReadLine();
        }
    }
}
