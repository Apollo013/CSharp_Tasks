# CSharp_Tasks
Demonstrates Asynchronous Programming with Tasks. Include file system and web access examples.
---
####Features
|Feature | Description |
|--------|------------|
|Task Creation | Demonstrates several ways for creating a task. |
|Task Chaining | Examples include passing data from the antecedent to the continuation, specifying the precise conditions under which the continuation will be invoked or not invoked, cancelling continuations, and more. |
|Child Tasks | Demonstrates attached & detached child tasks with 'TaskCreationOptions'|
|Exception Handling | Demonstrates attached & detached children exception handling, AggregateException Handling, AggregateException Flatten & handle methods, Task Exception Property |
|Task Cancellation| A couple of examples that demonstrates how to handle cancelling a task |
|Returning Values| Several methods that demonstrates how to return values from a Task. Also includes some PLINQ for file access. |
|Pre-Computed Tasks| Demonstrates how to use the Task.FromResult<TResult> method to retrieve the results of asynchronous download operations that are held in a cache.|
|Async & Await| Example demonstrates file access using async and await |

---

####Language Features
|Feature|
|-------|
|Task|
|Task< TResult >|
|Task.Run|
|Task.Factory.StartNew|
|Task< TResult >.Factory.StartNew|
|Task.FromResult|
|Task.Result|
|Task.Wait|
|Task.WaitAll|
|Task.ContinueWith|
|async & await|
|Func< Task,TResult >|
|Action< Task >|
|Lamda Expressions|
|Anonymous & named delagates|
|TaskContinuationOptions|
|TaskCreationOptions|
|AggregateException|
|AggregateException Handle method|
|AggregateException Flatten method|
|OperationCanceledException|
|CancellationTokenSource|
|CancellationTokenSource CancelAfter|
|Task Exception Property|
|System.IO.Directory.GetFiles|
|Timer|
|Stopwatch|
|ConcurrentDictionary|
|HttpClient|
|FileStream|

---
####Resources
| Title | Author | Publisher / Website |
|--------------|---------|--------|
| Async in C# 5.0| Alex Davies | O'Reilly |
| Pro C# 5.0 and the .NET 4.5 Framework| Andrew Troelsen | APRESS |
| [Walkthrough: Accessing the Web by Using async and await (C#)](https://msdn.microsoft.com/en-us/library/mt674891.aspx)|  | MSDN |
| [Task Parallelism (Task Parallel Library)](https://msdn.microsoft.com/en-us/library/dd537609(v=vs.110).aspx) |  | MSDN |
| [Chaining Tasks by Using Continuation Tasks](https://msdn.microsoft.com/en-us/library/ee372288(v=vs.110).aspx) |  | MSDN |
| [Exception Handling (Task Parallel Library)](https://msdn.microsoft.com/en-us/library/dd997415(v=vs.110).aspx) |  | MSDN |
| [How to: Return a Value from a Task](https://msdn.microsoft.com/en-us/library/dd537613%28v=vs.110%29.aspx) |  | MSDN |
| [Task Cancellation](https://msdn.microsoft.com/en-us/library/dd997396%28v=vs.110%29.aspx) |  | MSDN |
| [How to: Create Pre-Computed Tasks](https://msdn.microsoft.com/en-us/library/hh228607(v=vs.110).aspx) |  | MSDN |
| [Using Async for File Access (C#)](https://msdn.microsoft.com/en-us/library/mt674879.aspx) |  | MSDN |
| [Attached and Detached Child Tasks](https://msdn.microsoft.com/en-us/library/dd997417(v=vs.110).aspx)| | MSDN |
| [Task Parallelism In Multithreading](http://www.c-sharpcorner.com/UploadFile/1c8574/task-parallelism-in-multithreading/)| Gul Md Ershad | C# Corner |
|[C# – Race conditions and how to avoid it ?](http://www.csharpstar.com/csharp-race-conditions-in-threading/)| | Csharpstar |

