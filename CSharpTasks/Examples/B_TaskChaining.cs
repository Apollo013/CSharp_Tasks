using CSharpTasks.Utilities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpTasks.Examples
{
    public class B_TaskChaining
    {
        public static void Run()
        {
            PrintUtility.PrintTitle("TASK CHAINING");
            /*
            SingleAntecedentExample();
            ChainingWithActionTask();
            ChainingWithTResultFuncTask();
            ChainingUnderSpecificOptions();
            */
            ChainingWithCancellation();
        }

        private static void SingleAntecedentExample()
        {
            PrintUtility.PrintSubTitle("Chaining with Single Antecedent");
            // Execute the antecedent.
            Task<DayOfWeek> taskA = Task.Run(() => DateTime.Today.DayOfWeek);

            // Execute the continuation when the antecedent finishes.
            Task continuation = taskA.ContinueWith(antecedent => Console.WriteLine($"Today is {antecedent.Result}."));
            taskA.Wait();
        }

        private static void ChainingWithActionTask()
        {
            // Simply pass the result of one task to the next
            PrintUtility.PrintSubTitle("Chaining With Action<Task>");
            Task.Factory.StartNew(
                () =>
                {
                    int A = 6, B = 6, total1 = 0;
                    total1 = A + B;
                    Console.WriteLine($"Total 1: {A} + {B} = {total1}");
                    return total1;
                })
                .ContinueWith((total1) =>
                {
                    int divideby = 3;
                    int total2 = (total1.Result / divideby);
                    Console.WriteLine($"Total 2: ({total1.Result}) divided by {divideby} equals {total2} ");
                })
                .Wait();
        }

        private static void ChainingWithTResultFuncTask()
        {
            // Similar to 'ChainingWithActionTask' except this timne we use a Func delegate
            PrintUtility.PrintSubTitle("Chaining With Func<Task, TResult>");
            Func<int, int> negate =
                (n) =>
                {
                    Console.WriteLine($"Task={Task.CurrentId}, n={n}, -n={-n}, Thread={Thread.CurrentThread.ManagedThreadId}");
                    return -n;
                };


            Task<int>.Run(() => negate(5))
                .ContinueWith(antecendent => negate(antecendent.Result))
                .ContinueWith(antecendent => negate(antecendent.Result))
                .Wait();
        }

        private static void ChainingUnderSpecificOptions()
        {
            // Only certain continuation tasks will be called depending on the outcome of the parent task
            PrintUtility.PrintSubTitle("Chaining with specific Options");

            // ---------------------------------------------------------------------------------------------------------------------------
            Action success =
                () =>
                {
                    Console.WriteLine($"Task={Task.CurrentId}, Thread={Thread.CurrentThread.ManagedThreadId}: Begin successful transaction");
                };

            Action failure =
                () =>
                {
                    Console.WriteLine($"Task={Task.CurrentId}, Thread={Thread.CurrentThread.ManagedThreadId}: Begin transaction and encounter an error");
                    throw new InvalidOperationException("SIMULATED EXCEPTION");
                };

            Action<Task> commit =
                (antecedent) =>
                {
                    Console.WriteLine($"Task={Task.CurrentId}, Thread={Thread.CurrentThread.ManagedThreadId}: Commit transaction");
                };

            Action<Task> rollback = 
                (antecendent) =>
                {
                    // "Observe" your antecedent's exception so as to avoid an exception being thrown on the finalizer thread
                    var unused = antecendent.Exception;
                    Console.WriteLine($"Task={Task.CurrentId}, Thread={Thread.CurrentThread.ManagedThreadId}: Rollback transaction");
                };
            // ---------------------------------------------------------------------------------------------------------------------------
            PrintUtility.PrintSubTitle("Demonstrating a successful transaction");

            // Treated as "fire-and-forget" -- any exceptions will be cleaned up in rollback continuation
            Task t1 = Task.Factory.StartNew(success);

            // The following task gets scheduled only if tran1 completes successfully
            var commitTran1 = t1.ContinueWith(commit, TaskContinuationOptions.OnlyOnRanToCompletion);

            // The following task gets scheduled only if tran1 DOES NOT complete successfully
            var rollbackTran1 = t1.ContinueWith(rollback, TaskContinuationOptions.NotOnRanToCompletion);

            commitTran1.Wait();

            // ---------------------------------------------------------------------------------------------------------------------------
            PrintUtility.PrintSubTitle("Demonstrating a failed transaction");

            // Treated as "fire-and-forget" -- any exceptions will be cleaned up in rollback continuation
            Task t2 = Task.Factory.StartNew(failure);

            // The following task gets scheduled only if tran2 completes successfully
            var commitTran2 = t2.ContinueWith(commit, TaskContinuationOptions.OnlyOnRanToCompletion);

            // The following task gets scheduled only if tran2 DOES NOT complete successfully
            var rollbackTran2 = t2.ContinueWith(rollback, TaskContinuationOptions.NotOnRanToCompletion);

            rollbackTran2.Wait();
        }

        #region Cancelation
        private static void ChainingWithCancellation()
        {
            // ----------------------------------------------------------
            // COMMENTS
            // ----------------------------------------------------------
            /*
             * The task and its continuation represent two parts of the same logical operation so we can pass the same cancellation token to both tasks
             * It consists of an antecedent that generates a list of integers that are divisible by 33, which it passes to the continuation. 
             * The continuation in turn displays the list. Both the antecedent and the continuation pause regularly for random intervals. 
             * In addition, a System.Threading.Timer object is used to execute the Elapsed method after a five-second timeout interval. 
             * This calls the CancellationTokenSource.Cancel method, which causes the currently executing task to call the 
             * CancellationToken.ThrowIfCancellationRequested method. 
             * Whether the CancellationTokenSource.Cancel method is called when the antecedent or its continuation is executing depends on the duration 
             * of the randomly generated pauses. If the antecedent is canceled, the continuation will not start. 
             * If the antecedent is not canceled, the token can still be used to cancel the continuation. 
             */

            // ----------------------------------------------------------
            // VARS
            // ----------------------------------------------------------
            Random r = new Random();
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            Timer timer = new Timer(Elapsed, cts, 5000, Timeout.Infinite);

            // ----------------------------------------------------------
            // TASK
            // ----------------------------------------------------------
            var t = Task.Run(
                () =>
                {
                    List<int> product33 = new List<int>();

                    for(int i = 1; i < Int16.MaxValue; i++)
                    {
                        // Check if cancelled
                        if (token.IsCancellationRequested)
                        {
                            Console.WriteLine("\nCancellation requested in antecedent");
                            token.ThrowIfCancellationRequested();
                        }

                        // Delay Tactic
                        if(i % 2000 == 0)
                        {
                            int delay = r.Next(16, 501);
                            Thread.Sleep(delay);
                        }

                        // Add to lost if i is a product of 33
                        if(i % 33 == 0)
                        {
                            product33.Add(i);
                        }                        
                    }
                    // Return Result so that it can be passed to our continuation
                    return product33.ToArray();

                }, token);

            // ----------------------------------------------------------
            // CONTINUATION
            // ----------------------------------------------------------
            Task continuation = t.ContinueWith(
                antecedent =>
                {
                    Console.WriteLine("Multiples of 33:");

                    // Grab the arry from the antecedent
                    var arr = antecedent.Result;

                    for(int i = 0; i < arr.Length; i++)
                    {
                        // Check if cancelled
                        if (token.IsCancellationRequested)
                        {
                            Console.WriteLine("\nCancellation requested in continuation");
                            token.ThrowIfCancellationRequested();
                        }

                        // Delay Tactic
                        if (i % 100 == 0)
                        {
                            int delay = r.Next(16, 251);
                            Thread.Sleep(delay);
                        }

                        Console.Write("{0:N0}{1}", arr[i], i != arr.Length - 1 ? ", " : "");

                        // Write no further the column 74 (approx)
                        if (Console.CursorLeft >= 74)
                        {
                            Console.WriteLine();
                        }                            
                    }
                    Console.WriteLine();
                }, token);

            // ----------------------------------------------------------
            // RUN IT !
            // ----------------------------------------------------------
            try
            {
                continuation.Wait();
            }
            catch (AggregateException e)
            {
                Console.WriteLine();
                foreach (Exception ie in e.InnerExceptions)
                {
                    Console.WriteLine($"{ie.GetType().Name}: {ie.Message}");
                }                    
            }
            finally
            {
                cts.Dispose();
            }

            Console.WriteLine("\nAntecedent Status: {0}", t.Status);
            Console.WriteLine("Continuation Status: {0}", continuation.Status);
        }

        private static void Elapsed(object state)
        {
            CancellationTokenSource cts = state as CancellationTokenSource;
            if (cts == null)
            {
                return;
            }
            cts.Cancel();
            Console.WriteLine("\n\nCancellation request issued.");
        }
        #endregion
    }
}
