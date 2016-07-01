using CSharpTasks.Exceptions;
using CSharpTasks.Utilities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpTasks.Examples
{
    /// <summary>
    /// Demonstrates AggregateException for attached & detached children
    /// </summary>
    public class D_ExceptionHandling
    {

        public static void Run()
        {
            PrintUtility.PrintTitle("EXCEPTION HANDLING");
            //SimpleAggregateExample();
            //TaskExceptionProperty();
            //AggregateExceptionFlattenMethod();
            AggregateExceptionHandleMethod();
        }

        private static void SimpleAggregateExample()
        {
            PrintUtility.PrintSubTitle("SIMPLE AGGREGATED EXCEPTION HANDLING");

            var t1 = Task.Run( () =>
                {
                    throw new CustomException("WHAT THE HEY !!!");
                });

            try
            {
                t1.Wait();
            }
            catch(AggregateException ex)
            {
                foreach(var e in ex.InnerExceptions)
                {
                    if(e is CustomException)
                    {
                        Console.WriteLine(e.Message);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        private static void TaskExceptionProperty()
        {
            PrintUtility.PrintSubTitle("SIMPLE AGGREGATED EXCEPTION HANDLING");

            var t2 = Task.Run(() =>
                {
                    throw new CustomException("WHAT THE HEY !!!");
                });

            while (!t2.IsCompleted)
            {
                foreach(var e in t2.Exception.InnerExceptions)
                {
                    if (e is CustomException)
                    {
                        Console.WriteLine(e.Message);
                    }
                    else
                    {
                        throw e;
                    }
                }
            }
        }

        private static void AggregateExceptionFlattenMethod()
        {
            // Parent Task
            var t3 = Task.Factory.StartNew(() =>
            {
                // Child Task
                var c1 = Task.Factory.StartNew(() =>
                {
                    // Grand Child Task
                    var gc1 = Task.Factory.StartNew(() =>
                    {
                        // This exception is nested inside three AggregateExceptions.
                        throw new CustomException("Attached Grand Child faulted.");
                    }, TaskCreationOptions.AttachedToParent);

                    // This exception is nested inside two AggregateExceptions.
                    throw new CustomException("Attached child faulted.");
                }, TaskCreationOptions.AttachedToParent);
            });

            try
            {
                t3.Wait();
            }
            catch(AggregateException ex)
            {
                foreach(var e in ex.Flatten().InnerExceptions)
                {
                    if (e is CustomException)
                    {
                        Console.WriteLine(e.Message);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        private static void AggregateExceptionHandleMethod()
        {
            PrintUtility.PrintSubTitle("SIMPLE AGGREGATED EXCEPTION HANDLING");

            var t4 = Task.Run(() =>
            {
                throw new CustomException("WHAT THE HEY !!!");
            });

            try
            {
                t4.Wait();
            }
            catch(AggregateException ex)
            {
                ex.Handle(e =>
                {
                    if(e is CustomException)
                    {
                        Console.WriteLine(ex.Message);                       
                    }
                    return e is CustomException;
                });
            }
        }
    }
}
