using CSharpTasks.Models;
using CSharpTasks.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpTasks.Examples
{
    public class F_ReturningValues
    {
        public static void Run()
        {
            PrintUtility.PrintTitle("RETURNING VALUES");
            // Return a value type with a lambda expression
            Task<int> task1 = Task<int>.Factory.StartNew(() => 1);
            int i = task1.Result;


            // Return a named reference type with a multi-line statement lambda.
            Task<ReturnValueTest> task2 = Task<ReturnValueTest>.Factory.StartNew(() =>
            {
                string s = ".NET";
                double d = 4.0;
                return new ReturnValueTest { Name = s, Number = d };
            });
            ReturnValueTest test = task2.Result;
            Console.WriteLine(test.ToString());

            // Return an array produced by a PLINQ query
            Task<string[]> task3 = Task<string[]>.Factory.StartNew(() =>
            {
                string path = @"C:\Users\Public\Pictures\Sample Pictures\";
                string[] files = System.IO.Directory.GetFiles(path);

                var result = (from file in files.AsParallel()
                              let info = new System.IO.FileInfo(file)
                              where info.Extension == ".jpg"
                              select file).ToArray();

                return result;
            });

            foreach (var name in task3.Result)
            {
                Console.WriteLine(name);
            }
                
        }
    }
}
