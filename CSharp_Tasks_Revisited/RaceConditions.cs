using System;
using System.Threading.Tasks;

namespace CSharp_Tasks_Revisited
{
    public class RaceConditions
    {
        private static int counter = 0;

        public static void Run()
        {
            Task t1 = Task.Factory.StartNew(RunProblem);
            Task t2 = t1.ContinueWith(ante => RunSolutionWithContinueWith());
            Task.WaitAll(new Task[] { t1, t2 });

            Console.ReadLine();
        }

        private static void RunProblem()
        {
            Print("The Problem");
            Task.Run(() => PrintNumbers());
            Task.Run(() => PrintLetters());
        }


        private static void RunSolutionWithContinueWith()
        {
            Print("'ContinueWith' Solution");
            Task t1 = Task.Factory.StartNew(PrintNumbers);
            Task t2 = t1.ContinueWith(ante => PrintLetters());
            Task.WaitAll(new Task[] { t1, t2 });
        }

        private static void PrintNumbers()
        {
            for (counter = 1; counter <= 5; counter++)
            {
                Console.WriteLine(counter);
            }
        }

        private static void PrintLetters()
        {
            for (counter = 65; counter <= 70; counter++)
            {
                char chr = (char)counter;
                Console.WriteLine(chr);
            }
        }

        private static void Print(string title)
        {
            string divider = new string('-', 100);
            Console.WriteLine();
            Console.WriteLine(divider);
            Console.WriteLine(title);
            Console.WriteLine(divider);
        }
    }
}
