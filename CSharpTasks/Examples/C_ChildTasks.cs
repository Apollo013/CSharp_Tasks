using CSharpTasks.Utilities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpTasks.Examples
{
    /// <summary>
    /// Attached & Detached Child Tasks
    /// </summary>
    public class C_ChildTasks
    {
        public static void Run()
        {
            PrintUtility.PrintTitle("CHILD TASKS");
            AttachedChildTasks();
            DetachedChildTasks();
        }

        private static void AttachedChildTasks()
        {
            PrintUtility.PrintSubTitle("Attached Child Tasks");

            var t = Task.Factory.StartNew(
                () =>
                {
                    Console.WriteLine($"Running antecedent task {Task.CurrentId}");
                    Console.WriteLine("Launching attached child tasks...");

                    // Attach Kids
                    for (int i = 1; i <= 5; i++)
                    {
                        int idx = i;
                        Task.Factory.StartNew(
                            (value) =>
                            {
                                Console.WriteLine($"   Attached child task #{value} running");
                                Thread.Sleep(1000);
                            }, idx, TaskCreationOptions.AttachedToParent); // Attach Child Here
                    }
                    Console.WriteLine("Finished launching attached child tasks.");
                });

            var continuation = t.ContinueWith(
                (antecedent) =>
                {
                    Console.WriteLine($"Executing continuation of Task {antecedent.Id}");
                });

            continuation.Wait();
        }

        private static void DetachedChildTasks()
        {
            PrintUtility.PrintSubTitle(" Detached Child Tasks");

            var t = Task.Factory.StartNew(
                () =>
                {
                    Console.WriteLine($"Running antecedent task {Task.CurrentId}");
                    Console.WriteLine("Launching detached child tasks...");

                    // Attach Kids
                    for (int i = 1; i <= 5; i++)
                    {
                        int idx = i;
                        Task.Factory.StartNew(
                            (value) =>
                            {
                                Console.WriteLine($"   detached child task #{value} running");
                                Thread.Sleep(1000);
                            }, idx); // NOT ATTACHED
                    }
                    Console.WriteLine("Finished launching detached child tasks.");
                }, TaskCreationOptions.DenyChildAttach); // DENY CHILDREN

            var continuation = t.ContinueWith(
                (antecedent) =>
                {
                    Console.WriteLine($"Executing continuation of Task {antecedent.Id}");
                });

            continuation.Wait();
        }
    }
}
