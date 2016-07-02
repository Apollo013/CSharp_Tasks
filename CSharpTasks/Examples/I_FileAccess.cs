using CSharpTasks.Utilities;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTasks.Examples
{
    /// <summary>
    /// Demonstrates asynchronous access of file content
    /// </summary>
    public class I_FileAccess
    {
        private static string filePath = @"temp2.txt";
        public static void Run()
        {
            //Task.Factory.StartNew(() => WriteOne()).Wait();
            Task.Factory.StartNew(() => ReadOne(filePath)).Wait();
        }

        #region WRITE
        private static async Task WriteOne()
        {
            PrintUtility.PrintSubTitle("Write");
            string text = "Hello World It's Me Again\r\n";
            await WriteAsync(filePath, text);
        }

        private static async Task WriteAsync(string filePath, string fileContent)
        {
            byte[] encodedText = Encoding.Unicode.GetBytes(fileContent);

            using (FileStream sourceStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            }
        }
        #endregion


        #region READ
        private static async Task ReadOne(string filePath)
        {
            PrintUtility.PrintSubTitle("Read");
            if (File.Exists(filePath))
            {
                try
                {
                    string fileContent = await ReadAsync(filePath);
                    Console.WriteLine(fileContent);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine($"file not found: {filePath}");
            }
        }

        private static async Task<string> ReadAsync(string filePath)
        {
            using (FileStream sourceStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
            {
                StringBuilder sb = new StringBuilder();

                byte[] buffer = new byte[0x1000];
                int numRead;
                while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string text = Encoding.Unicode.GetString(buffer, 0, numRead);
                    sb.Append(text);
                }

                return sb.ToString();
            }
        }
        #endregion
    }
}
