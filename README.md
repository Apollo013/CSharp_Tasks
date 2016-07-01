# CSharp_Tasks
Demonstrates Asynchronous &amp; Parallel Programming with Tasks. Examples include file system access and downloading from the web.
---
####Features
|Feature | Description |
|--------|------------|
|Task Creation | Demonstrates several ways to start a thread. Features Task.Factory.StartNew, Task.Run, Task.Wait, Action Delegate, Anonymous Delegate, lamda experessions, async & await. |
|Task Chaining | Examples include passing data from the antecedent to the continuation, specifying the precise conditions under which the continuation will be invoked or not invoked and more. Features ContinueWith Action< Task >, ContinueWith Func< Task,TResult >, and 'TaskContinuationOptions'. |

---

####Method & Struct Features
|Feature|
|-------|
|Task.Run|
|Task.Factory.StartNew|
|Task.Wait|
|Task.ContinueWith|
|Func|
|Action|

---
####Resources
| Title | Source | Type |
|--------------|---------|--------|
| [Walkthrough: Accessing the Web by Using async and await (C#)](https://msdn.microsoft.com/en-us/library/mt674891.aspx)| MSDN | Website |
| [Task Parallelism (Task Parallel Library)]( https://msdn.microsoft.com/en-us/library/dd537609(v=vs.110).aspx) | MSDN | Website |
| [Chaining Tasks by Using Continuation Tasks]( https://msdn.microsoft.com/en-us/library/ee372288(v=vs.110).aspx) | MSDN | Website |
| Async in C# 5.0| Alex Davies | Published By O'Reilly |
