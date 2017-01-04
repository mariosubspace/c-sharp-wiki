# Main

Some variations of the Main method.

```cs
static void Main() { }
static int Main() { return 0; }
static int Main(string[] args) { return 0; }
```

Command-line args can also be retrieved using `System.Environment.GetCommandLineArgs()`.



# Console I/O

```cs
Console.WriteLine();
Console.ReadLine();
```



# Strings

### Formatting

```cs
string.Format("{0} {1} {2}", a, b, c);
string.Format("{0:d9}", someNumber);
```

The second example shows how specific formatting can be applied. That is, limiting decimal place precision, padding, etc.

### Verbatim Strings

With "verbatim" strings, no escaping is done (\n, \t, etc).

```cs
string v = @"This is a ""verbatim"" string. Backslashes, \, are not escaped.";
```

### Interpolated Strings
_.NET 4.6+_

With "interpolated" strings, variables can be referenced in the string and filtered.

```cs
string p = $"myVariable has value {myVariable | expression}."
```



# Arrays

Various ways to initialize.
```cs
int[] a = {1, 2, 3};
int[] b = new int[] {1, 2, 3};
int[] c = new int[3];
```

Implicitly-typed
```cs
var a = new[] {1, 2, 3};
```

### Matrix

```cs
int[,] mat = new int[3, 4];
```

### Jagged Array

```cs
int[][] jag = new int[3][];
for (int i = 0; i < 3; ++i)
{
  jag[i] = new int[4];
}
```



# Parameters

### Modifiers

* `out` - method _must_ assign a value to the argument before returning. Not necessary for caller to initialize variable.
* `ref` - method _may_ assign a value to the argument. Necessary for caller to initialize variable.
* `params` - list of parameters.

###### Example 1 (out)

```cs
void Add(int a, int b, out int sum)
{
  sum = a + b;
}
```

```cs
int sum;
Add(4, 5, out sum);
```

###### Example 2 (ref)

```cs
void Swap(ref Card a, ref Card b)
{
  Card tmp = a;
  a = b;
  b = tmp;
}
```

```cs
Card a = new Card("KD");
Card b = new Card("9D");
Swap (ref a, ref b);
```

###### Example 3 (params)

```cs
int Sum(params int[] nums)
{
  int sum = 0;
  foreach (int i in nums)
  {
    sum += i;
  }
  return sum;
}
```

```cs
int result = Sum(1, 2, 3, 4);
```

### Default Parameters

```cs
void LogEvent(string msg, string tag = "Default", Color color = Color.blue);
```

You can use **named parameters** to feed arguments out of order.

```cs
LogEvent(tag: "Timing");
```

Unnamed, ordered parameters must come first though.

```cs
LogEvent(msg, color: Color.red);
```

You can also use default parameters in constructors above .NET 4.0.



# Enums

```cs
enum MyEnum { Yo }
```

```cs
enum MyEnum
{
  Three = 3, // Will start with value 3.
  Four,
  Five
}
```

By default, enums use the `System.Int32` (C# `int`) type. You can change this as follows.
```cs
enum MyEnum : byte
{
  MyByte
}
```

You can get the name as a string or raw value like this.
```cs
MyEnum.Three.ToString(); // "Three"
(int)MyEnum.Three; // 3
```



# Value Types

* ValueTypes are extended from `Object`.
* ValueTypes declare their memory on the stack instead of the heap. This means that
a ValueType will be deallocated on exiting its scope in which it was defined.
In contrast to the managed heap where it would have to wait to be garbage collected.
* Primitive types, enums, and structs are implemented using ValueTypes.
* ValueTypes are copied member-by-member when assigned to a variable or passed as an
argument. If a member is a ReferenceType (what classes use) then only its reference
will be copied unless `IClonable` is implemented by the ReferenceType.

### Structs

Structs cannot extend classes, or override the default constructor.

Quirk: If you have private fields and properties in a struct with a custom constructor, then you need to call
the default constructor in all custom constructors to initialize the fields with default values.

```cs
struct Point
{
  private int X { get; set; }
  private int Y { get; set; }
  private string label;

  public Point(string label) : this()
  {
    this.label = label;
  }
}
```

### Nullable

ValueTypes cannot be assigned null. They must be wrapped in Nullable<T>.

```cs
System.Nullable<int> i = null;
```

There is a convenient shorthand for this (?).
```cs
int? i = null;
bool? b = null;
float? t = 0.1f;
```



# Null Operators

### Null Coalescing Operator (??)
_Returns an alternate value on null._

```cs
Card cardB = cardA ?? new Card("2C"); // If cardA is null, return new Card.
```

### Null Conditional Operator (?.)
_Provides safe method access. Also known as "Elvis operator."_

```cs
Card cardA = deckA?.Get(1);
```

If `deckA` turns out to be null, it will return null and not call `Get()`.



# Constructors

### Chaining

```cs
public Card(char rank, char suit) { } // Master constructor.
public Card() : this('2', 'C') { } // Use master constructor.
```

### Static

You can initialize static variables with the static constructor.

```cs
static MyStaticConstructor()
{
  // No access modifiers allowed.
}
```

This is called only once on first instantiation of the class, or on static member access.



# Static Import

You can import static members with `using static`.

```cs
using static ClassWithStaticMembers;
```



# Access Modifiers

* `public` - Open to everyone.  
* `private` - Only members of the class.  
* `protected` - private + children.  
* `internal` - Only within the assembly.  
* `protected internal`- Protected and within the assembly (no external children).  
* Types are implicitly internal.  
* Members are implicitly private.  



# Properties

Properties are an implementation of the accessor/mutator (getter/setter) pattern.

A basic .NET property:
```cs
public int SomeInt
{
  get { return myInt; }
  set {  myInt = value; }
}
```

### Auto-Properties

```cs
public int SomeInt { get; set; }
```

This uses a private field internally and sets that to a default value.

For ReferenceTypes, the default value is null; which can be a problem. In many cases,
the default value will not be what you want. The solution to this is to assign a default
value in the constructor. Alternatively, C# 6.0 includes a special syntax.

```cs
public string SomeString { get; set; } = "Default value.";
```



# Object Initialization Syntax

You can use object-literal notation in constructing an object.
```cs
Card c = new Card { Rank = '5', Suit = 'D' };
Card c = new Card() { Rank = '5', Suit = 'D' }; // Equivalent to above.
Card c = new Card(suit: 'D') { Rank = '5' }; // You can get crazy with this.
```

The names in the brackets are public fields of the Card class, not the names
of the parameters in the constructor.



# Read-Only Fields

* A `const` field must be assigned at the time of the declaration.
* A `readonly` field can be assigned at declaration or in the constructor.

`const` is implicitly static.
```cs
public const int PI = 3.1415f;
```

`readonly` is an instance variable.
```cs
public readonly DateTime INITIALIZED;

public MyBrand()
{
  INITIALIZED = DateTime.Now;
}
```

`static` can be used with `readonly`, which is almost like `const`
```cs
public static readonly DateTime FIRST_INITIALIZATION;

static MyBrand()
{ // The difference is you can use a static constructor to initialize it.
  FIRST_INITIALIZATION = DateTime.Now;
}
```



# Partial classes

The `partial` keyword allows classes to be split among multiple files. The file names don't matter,
only that the class name and namespace are the same.

This is one way you could split things up:

```cs
// Cookie.cs
partial class Cookie
{
  // Methods
  // Properties
}
```

```cs
// Cookie.Boilerplate.cs
partial class Cookie
{
  // Field data
  // Constructors
}
```



# The 'Sealed' Keyword

This keyword prevents a class from being extended.
```cs
sealed class MySealedClass { }
```



# The 'Base' Keyword

Just like Java's `super`, C# has the `base` keyword to call a parent ctor.
```cs
public MyClass(int a, string b) : base(a)
{
  this.b = b;
}
```

You of course can use `base` to access members of the base class as well.



# Polymorphism

You can mark a method as overrideable with the `virtual` keyword.
```cs
public virtual void OverrideableMethod() { }

// Then, in the child class:
public override void OverrideableMethod() { }
```

You can call the parent method with:
```cs
base.OverrideableMethod();
```

You can prevent a method from further being overridden by sealing it.
```cs
public override sealed void OverrideableMethod() { }
```



# Abstract Classes

```cs
abstract class MyAbstractClass
{
  public abstract void AbstractMethod();
}

class AnotherClass : MyAbstractClass
{
  public override void AbstractMethod() { /* ... */ }
}
```



# Interfaces

C# interfaces are similar to Java interfaces.

```cs
public interface IClashing
{
  public void ClashingMethod();
}
```

If by chance you have methods in different interfaces that clash, you can define
them explicitly.
```cs
class SomeClass : IClashing, IAlsoClashing
{
  public void IClashing.ClashingMethod() { }
  public void IAlsoClashing.ClashingMethod() { }
}
```

You need to explicitly cast to the specific interface type to use a clashing method.
```cs
(someClass as IClashing)?.ClashingMethod();

IClashing ic = ((IClashing)someClass);
if (ic) ic.ClashingMethod();
```

If you just define the unqualified clashing method, it will override all of them.

### ICloneable

C#'s object class defines a protected `MemberwiseClone()` method that copies an
object by copying the members.

To perform a deep copy you can utilize `ICloneable`.
```cs
public interface ICloneable
{
  object Clone(); // Just create a new object and copy the values yourself.
}
```

### IComparable and IComparer

`IComparable` is notable utilized by `Arrays.Sort`.
```cs
public interface IComparable
{
  int CompareTo(object o); // -1 (lt), 0 (eq), 1 (gt).
}
```

`IComparer` is typically defined on a helper class, (e.g., ISBNComparer.Compare(Book a, Book b)).
`Arrays.Sort` also can use this: `Arrays.Sort(books, new ISBNComparer())`.
```cs
public interface IComparer
{
  int Compare(object a, object b);
}
```



# IEnumerable, IEnumerator, Iterators, and Yield

Note: these examples don't use the generic versions. Use the generic versions to
prevent penalties incurred by auto-boxing.

```cs
public interface IEnumerable
{
  IEnumerator GetEnumerator(); // Used by the foreach loop.
}

public interface IEnumerator
{
  bool MoveNext(); // Advance the cursor position.
  object Current() { get; } // Get the current item.
  void Reset(); // Reset the cursor.
}

public class SomeClass : IEnumerable
{
  // ...
  // This is a special "iterator" method.
  public IEnumerator GetEnumerator()
  {
    for (var i = 0; i < items.Length; ++i)
    {
		 // The compiler automatically generates a nested IEnumerator that
		 // keeps track of execution, and returns items[i] for Current.
		 yield return items[i];
		 // You cannot call Reset on an instance of this generated IEnumerator,
		 // you'll have to get a new instance of it.
    }
  }

  // This is a "named iterator" method.
  public IEnumerator GetReverseEnumerator(string msg)
  {
    Console.WriteLine("Named iterators can receive parameters: " + msg);
    for (var i = items.Length-1; i >= 0; --i)
    {
      yield return items[i];
    }
  }
}
```

You can use the iterators in a `foreach` loop.
```cs
foreach (Item it in someClass) {}
foreach (Item it in someClass.GetReverseEnumerator()) {}
```

### The 'Yield' Keyword

When you use the yield keyword in a statement, you indicate that the method,
operator, or "get" accessor in which it appears is an iterator. Using yield
to define an iterator removes the need for an explicit extra class (the class
that holds the state for an enumeration, see IEnumerator<T>)

* `yield return` - return each element one at a time.  
* `yield break` - end iteration.

You consume an iterator by using a foreach statement or LINQ query.
Each iteration of the foreach loop calls the iterator. When a
`yield return` statement is reached in the iterator method, the expression is
returned, and the current location in code is retained. Execution is
restarted from that location the next time that the iterator function is
called.

`yield` is not a reserved word and has special meaning only when
it is used before a `return` or `break` keyword.

An iterator method cannot have any `ref` or `out` parameters.

An implicit conversion must exist from the expression type in the `yield return`
statement to the return type of the iterator.



# Class Cast Checking

### The 'AS' Keyword
_Null if not type._
```cs
Deck d = someObject as Deck;
if (d != null) d.Shuffle();
```

### The 'IS' Keyword
_False if not type._
```cs
if (someObject is Deck)
{
  ((Deck)someObject).Shuffle();
}
```



# Boxing

Every time you assign a `ValueType` to a `ReferenceType`, e.g.,

```cs
int f = 5;
object oa = f;
```

C# does a 'box' operation: allocates an object on the heap and moves the value from the stack
to the new object.

C# does an 'unbox' operation to do the inverse, and requires casting:

```cs
int g = (int)oa;
```

This is very inefficient speed-wise and memory-wise.



# Shadowing

Instead of overriding, you can completely replace (shadow) the parent method, property or field.
```cs
public new int MyProperty { get; set; }
public new void MyMethod() { }
```

You can still access the parent's original member by explicit casting.
```cs
((Parent)child).MyMethod();
```



# Exceptions

Fatal system exceptions extend `SystemException` and are thrown by the CLR. Extend
`ApplicationException`, or just `Exception`, for app-related exceptions.

You don't have to specify an exception in try-catch.
```cs
try { }
catch { }
```

If you do, the syntax is the same as in Java.
```cs
try { }
catch (Exception e) { }
```

You can rethrow an exception by just calling throw in the catch block.
```cs
try {}
catch
{
  // Stuff.
  throw;
}
```

Throwing exceptions is the same as in Java too.
```cs
throw new SomeException();
```

If an exception happens while handling another, the proper practice is to shove
the inner exception in a new one of the parent type.
```cs
try { }
catch (CustomException ce)
{
  try { }
  catch (Exception ie)
  {
    throw new CustomException(ce.Message, ie);
  }
}
```

### Exception Filters

Use `when` to conditionally run code in a catch block.
```cs
try {}
catch (Exception e) when (isDebuggingOn) {}
```



# Generics

Generics provide better performance because they prevent boxing or unboxing
penalties when storing ValueTypes.

```cs
// Generic method.
void AddToList<T>(T item)
{
  Console.Log("Adding item of type " + typeof(T));
}

// Generic struct.
public struct Point<T>
{
  public void ResetPoint()
  {
    x = default(T); // Sets the default value w.r.t. the generic type.
    y = default(T);
  }
}
```

### Generic Constraints

Generic class using constraints on the generic type.
```cs
public class MyGenericClass<T> where T : class, IDrawable, new()
{ }
```

**Possible Constraints:**
* `struct` - ValueTypes  
* `class` - ReferenceTypes  
* `new()` - Must have a default ctor. Must be last in the list.  
* `MyCustomType`  
* `IMyInterface` - Can have multiple interface constraints.  
* `U` - Depends on generic type 'U' (generic param. U must also specified). Does not have to be letter 'U' of course.  

```cs
class MyGeneric<T, U>
  where T : Set<U>, ISomeInterface
  where U : struct
  { }
```

Notice the `where TypeParam : [constraints]` sections are _not_ separated by commas.

### The System.Collection.Generics Namespace

#### Generic Interfaces

* `ICollection<T>` - Defines general characteristics (e.g., size, enumeration, and thread saftey)
for all generic collection types.  
* `IComparer<T>`  
* `IDictionary<T>` - Allows a generic collection object to represent its contents using key-value pairs.  
* `IEnumerable<T>`  
* `IEnumerator<T>`  
* `IList<T>`  
* `ISet<T>`  

#### Generic Collections

* `Dictionary<TKey, TValue>`  
* `LinkedList<T>`  
* `List<T>`  
* `Queue<T>`  
* `SortedDictionary<TKey, TValue>`  
* `SortedSet<T>`  
* `Stack<T>`

### The System.Collections.ObjectModel Namespace

This contains a couple of important collection objects that notify listeners when the collection is modified.

* `ObservableCollection<T>`  
* `ReadOnlyObservableCollection<T>`  



# Delegates

```cs
// Declaring a delegate type.
public delegate string MyDelegate(bool b, int n);

// Declaring and initializing a delegate variable.
MyDelegate mdel = new MyDelegate(SomeMethod);

// Alternatively, you can just supply the method name.
// This is called "method group conversion syntax".
MyDelegate mdel = SomeMethod;

// Delegates can be combined to call multiple methods.
mdel += AnotherMethod;
mdel += OrAnotherDelegate;

// Invoking the delegate.
mdel();
```

### Generic Delegates

```cs
public delegate void GenericDel<T>(T arg);

public void MyFunc(string a) { }
GenericDel<string> strTarget = MyFunc;
```

### Action<...> and Func<...>

These two are out-of-the-box, generic delegate types for simplifying the definition of a delegate.

#### Action<...>

* Takes up to 16 arguments.  
* Can only point to functions with a `void` return type.  

```cs
static void DisplayMessage(string msg, ConsoleColor col) { }
Action<string, ConsoleColor> target = DisplayMessage;
target("hello", ConsoleColor.Blue);
```

As you can see in the example, the types in the generic parameter list define the
types of the parameters the delegate methods take in.

#### Func<...>

* Takes up to 16 arguments.  
* The last generic parameter is the return type.

```cs
static string IntToString(int a) { }
Func<int, string> target = IntToString;
string s = target(5);
```

### Predicate<T>

Defined in the System as such:
```cs
public delegate bool Predicate<T>(T obj);
```

This is used in places like `List.FindAll()`.

### Events

Events are a special kind of multicast delegate that can only be invoked
from within the class or struct where they are declared (the publisher class).

```cs
public delegate string MyDelegate(int a);
public event MyDelegate MyEvent;
```

### EventArgs

Microsoft's recommended pattern for how to publish data with a delegate.

```cs
public delegate void MyDelegate(object sender, MyEventArgs e);
public event MyDelegate MyEvent;

public class MyEventArgs
{
  public MyEventArgs(string s) { Text = s; }
  public string Text { get; private set; } // readonly
}

// Variation
public class MyEventArgs
{
  public MyEventArgs(string s) { text = s; }
  public readonly string text;
}

// Now when you raise an event to listeners. (In the publisher class...)
MyEvent(this, new MyEventArgs("Hey, this is some event data."));
```

#### EventHandler<T>

Given the recommended (object sender, EventArgs e) pattern, there is a generic
event type Microsoft provides to streamline this.

```cs
public event EventHandler<MyEventArgs> MyEvent;
```

You don't need to define the delegate in this case.

### Delegates: Behind the Scenes

The compiler generates a class for a delegate type.
```cs
sealed class MyDelegate : System.MulticastDelegate
{
  // Basic synchronous invocation.
	public string Invoke(bool b, int n);

	// The other two are for asynchronous use.
	public IAsyncResult BeginInvoke(bool b, int b, AsyncCallback cb, object state);
	public string EndInvoke(IAsyncResult result);
}
```

The `MulticastDelegate` class is defined as such:
```cs
public abstract class MulticastDelegate : Delegate
{
	// Returns the list of methods "pointed to"
	public sealed override Delegate[] GetInvocationList();

	// Overloaded operators.
	public static bool operator ==(MulticastDelegate d1, MulticastDelegate d2);
	public static bool operator !=(MulticastDelegate d1, MulticastDelegate d2);

	// Used internally to manage the list of methods maintained by the delegate.
	private IntPtr _invocationCount;
	private object _invocationList;
}
```

The `Delegate` class is defined as such:
```cs
public abstract class Delegate : ICloneable, ISerializable
{
	// Methods to interact with the list of functions.
	public static Delegate Combine(params Delegate[] delegates);
	public static Delegate Combine(Delegate a, Delegate b);
	public static Delegate Remove(Delegate source, Delegate value);
	public static Delegate RemoveAll(Delegate source, Delegate value);

	// Overloaded operators.
	public static bool operator ==(Delegate d1, Delegate d2);
	public static bool operator !=(Delegate d1, Delegate d2);

	// Properties that expose the delegate target.
	public MethodInfo Method { get; }
	public object Target { get; }
}
```

* The '+=' operator is syntax sugar for the Combine() method.  
* The '-=' operator is syntax sugar for the Remove() method.  
* 'Method' gets target method details.  
* 'Target' gets object details if the target method belongs to an instance.  



# Anonymous Methods

```cs
someDelegateOrEvent += delegate {
  Console.WriteLine("Anonymous method which ignores arguments.");
}

someDelegateOrEvent += delegate(int a, string b) {
  Console.WriteLine("Anonymous method which accepts arguments.");
}
```

* Cannot access `ref` or `out` parameters in the outer method.  
* Cannot have local variables that are the same as in the outer method.  
* Stuff in the outer class scope works as expected.  



### Lambda Expressions

Various forms of lambda expressions.
```cs
() => Console.WriteLine("Hello.");

i => (i % 2) == 0

(i) => (i % 2) == 0

(int i) => (i % 2) == 0

(a, b) => a + b

(a) => {
  Console.WriteLine(a);
  return a + 1;
}
```

As of .NET 4.6, you can use lambdas for member methods too.
```cs
class MyClass
{
  public int Add(int x, int y) => x + y;
}
```



# Overflow Checking

You can use the `checked` keyword to carry out a checked cast, which checks for numeric overflow when narrowing a scope.

Block form:
```cs
checked
{
  byte a = (byte)someIntegerA;
  byte b = (byte)someIntegerB;
}
```

Inline form:
```cs
checked(byte a = (byte)someIntegerA);
```

If you can enabled overflow checking project-wide, you can use the `unchecked` keyword in a similar fashion to ignore checking.

The project-wide setting is usually in "Build Settings > Check for arithmetic overflow/underflow".
