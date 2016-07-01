using System;

namespace CSharpTasks.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
    }
}
