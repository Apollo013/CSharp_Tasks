# CSharp_Tasks
Demonstrates Asynchronous &amp; Parallel Programming with Tasks. Examples include file system access and downloading from the web.
---
####Features
|Feature | Description |
|--------|------------|
|Task Creation | Demonstrates several ways for creating a task. |
|Task Chaining | Examples include passing data from the antecedent to the continuation, specifying the precise conditions under which the continuation will be invoked or not invoked, cancelling continuations, and more. |
|Child Tasks | Demonstrates attached & detached child tasks |
|Exception Handling | Demonstrates attached & detached children exception handling, AggregateException Handling, AggregateException Flatten & handle methods, Task Exception Property |
|Task Cancellation| A couple of examples that demonstrates how to handle cancelling a task |

---

####Language Features
|Feature|
|-------|
|Task|
|Task< TResult >|
|Task.Run|
|Task.Factory.StartNew|
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


---
####Resources
| Title | Source | Type |
|--------------|---------|--------|
| [Walkthrough: Accessing the Web by Using async and await (C#)](https://msdn.microsoft.com/en-us/library/mt674891.aspx)| MSDN | Website |
| [Task Parallelism (Task Parallel Library)](https://msdn.microsoft.com/en-us/library/dd537609(v=vs.110).aspx) | MSDN | Website |
| [Chaining Tasks by Using Continuation Tasks](https://msdn.microsoft.com/en-us/library/ee372288(v=vs.110).aspx) | MSDN | Website |
| [Exception Handling (Task Parallel Library)](https://msdn.microsoft.com/en-us/library/dd997415(v=vs.110).aspx) | MSDN | Website |
| Async in C# 5.0| Alex Davies | Published By O'Reilly |

