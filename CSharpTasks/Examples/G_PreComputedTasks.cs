using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CSharpTasks.Examples
{
    /// <summary>
    /// Demonstrates how to use the Task.FromResult<TResult> method to retrieve the results of 
    /// asynchronous download operations that are held in a cache.
    /// </summary>
    public class G_PreComputedTasks
    {
        // Holds the results of download operations.
        static ConcurrentDictionary<string, string> cachedDownloads = new ConcurrentDictionary<string, string>();

        public static void Run()
        {
            // The URLs to download.
            string[] urls = new string[]
            {
                "http://msdn.microsoft.com",
                "http://www.contoso.com",
                "http://www.microsoft.com"
            };

            // Used to time download operations.
            Stopwatch stopwatch = new Stopwatch();

            // Used for LINQ query
            IEnumerable<Task<string>> downloads = null;

            // Action delegate used to retrieve content
            Action GetDownloads = () =>
            {
                downloads = from url in urls select DownloadAsync(url);
                Task.WhenAll(downloads).ContinueWith(results =>
                {
                    stopwatch.Stop();
                    // Print the number of characters download and the elapsed time.
                    Console.WriteLine($"Retrieved {results.Result.Sum(result => result.Length)} characters. Elapsed time was {stopwatch.ElapsedMilliseconds} ms.");
                })
                .Wait();
            };


            // Compute the time required to download the URLs.
            stopwatch.Start();
            Task.Factory.StartNew(GetDownloads).Wait();

            // Perform the same operation a second time. The time required
            // should be shorter because the results are held in the cache.
            stopwatch.Restart();
            Task.Factory.StartNew(GetDownloads).Wait();

        }

        public static Task<string> DownloadAsync(string url)
        {
            // First try to retrieve the content from cache.
            string content;
            if(cachedDownloads.TryGetValue(url, out content))
            {
                return Task.FromResult<string>(content);
            }

            // If the result was not in the cache, download the content and add it to the cache.
            return Task.Run(async () =>
            {
                using(var client = new HttpClient())
                {
                    content = await client.GetStringAsync(url);
                    cachedDownloads.TryAdd(url, content);
                    return content;
                }
            });

        }
    }
}
