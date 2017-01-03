Some notes following the book "C# 6.0 and the .NET 4.6 Framework".  
This useful mostly for review if you just need to glance at some topics.  

# Main

Some variations of the Main method.

```csharp
static void Main() { }
static int Main() { return 0; }
static int Main(string[] args) { return 0; }
```

Command-line args can also be retrieved using `System.Environment.GetCommandLineArgs()`.

# Console I/O

```csharp
Console.WriteLine();
Console.ReadLine();
```

# Strings

### Formatting

```csharp
string.Format("{0} {1} {2}", a, b, c);
string.Format("{0:d9}", someNumber);
```

The second example shows how specific formatting can be applied. That is, limiting decimal place precision, padding, etc.

### Verbatim Strings

With "verbatim" strings, no escaping is done (\n, \t, etc).

```csharp
string v = @"This is a ""verbatim"" string. Backslashes, \, are not escaped.";
```

### Interpolated Strings
_.NET 4.6+_

With "interpolated" strings, variables can be referenced in the string and filtered.

```csharp
string p = $"myVariable has value {myVariable | expression}."
```
