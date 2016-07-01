using CSharpTasks.Utilities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpTasks.Examples
{
    public class E_TaskCancellation
    {
        public static void Run()
        {
            PrintUtility.PrintTitle("CANCELLATION HANDLING");
            BasicCancellationExample();
            CancelAfterExample();
        }

        private static void BasicCancellationExample()
        {
            PrintUtility.PrintSubTitle("BASIC EXAMPLE");

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;

            var task = Task.Factory.StartNew(() =>
            {
                // Were we already canceled?
                ct.ThrowIfCancellationRequested();

                bool moreToDo = true;
                while (moreToDo)
                {
                    // Poll on this property if you have to do
                    // other cleanup before throwing.
                    if (ct.IsCancellationRequested)
                    {
                        // Clean up here, then...
                        ct.ThrowIfCancellationRequested();
                    }

                }
            }, ct); // Pass same token to StartNew.

            cts.Cancel();

            // Just continue on this thread, or Wait/WaitAll with try-catch:
            try
            {
                task.Wait();
            }
            catch (AggregateException e)
            {
                foreach (var v in e.InnerExceptions)
                    Console.WriteLine(e.Message + " " + v.Message);
            }
            finally
            {
                cts.Dispose();
            }
        }

        private static void CancelAfterExample()
        {
            PrintUtility.PrintSubTitle("CANCEL AFTER EXAMPLE");

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;

            try
            {
                cts.CancelAfter(1000); // Throw after 1 sec
                Task<int> sum = Task.Factory.StartNew(() => Compute(5, cts.Token));
                Console.WriteLine($"Ok; Sum = {sum.Result}");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Computation canceled: TestTimeOut");
            }
            catch (Exception)
            {
                Console.WriteLine("Computation failed: TestTimeOut");
            }
            finally
            {
                cts.Dispose();
            }
        }

        private static int Compute(int num, CancellationToken ct)
        {
            Task.Delay(2000).Wait(); // Simulate a process longer than the 'cancel after' value
            ct.ThrowIfCancellationRequested();
            int sum = num * 2;
            return sum;
        }
    }
}
