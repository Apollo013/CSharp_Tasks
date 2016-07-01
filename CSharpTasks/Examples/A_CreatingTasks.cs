using CSharpTasks.Utilities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpTasks.Examples
{
    public class A_CreatingTasks
    {
        public static void Run()
        {
            PrintUtility.PrintTitle("TASK CREATION");

            PrintUtility.PrintSubTitle("FACTORY WITH ANONYMOUS DELEGATE WITH LAMDA");
            var tBase = Task.Factory.StartNew(() => { Console.WriteLine("Factory Method"); });
            tBase.Wait();


            PrintUtility.PrintSubTitle("ACTION DELEGATE");
            Task t1 = new Task(new Action(PrintMessage));
            t1.Start();
            t1.Wait();


            PrintUtility.PrintSubTitle("DELEGATE");
            Task t2 = new Task(delegate { PrintMessage(); });
            t2.Start();
            t2.Wait();


            PrintUtility.PrintSubTitle("LAMDA & NAMED METHOD");
            Task t3 = new Task(() => PrintMessage());
            t3.Start();
            t3.Wait();


            PrintUtility.PrintSubTitle("LAMDA & ANONYMOUS METHOD");
            Task t4 = new Task(() => { PrintMessage(); });
            t4.Start();
            t4.Wait();


            // The Run methods are the preferred way to create and start tasks when more control over the creation and scheduling of the task is not needed. 
            PrintUtility.PrintSubTitle("RUN METHOD");
            RunMethod();


            PrintUtility.PrintSubTitle("RUN METHOD: Where Run & Await calls are seperated");
            Thread.CurrentThread.Name = "Main";            
            Task t5 = Task.Run(() => Console.WriteLine("Hello from t5."));          // Define and run the task.            
            Console.WriteLine($"Hello from thread '{Thread.CurrentThread.Name}'."); // Output a message from the calling thread.
            t5.Wait();                                                              // Output a message from the task.
        }

        private static async void RunMethod()
        {
            await Task.Run(() => PrintMessage());
        }

        private static void PrintMessage()
        {
            Console.WriteLine("Hello Task library!");
        }
    }
}
