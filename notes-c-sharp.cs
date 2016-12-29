/*** MAIN ***/

static void Main() {}
static int Main() { return 0; }
static int Main(string[] args) { return 0; }

// Can be used to get command-line args.
System.Environment.GetCommandLineArgs();


/** CONSOLE IO */
Console.WriteLine();
Console.ReadLine();


/*** STRINGS ***/

// You can use,
string.Format("{0} {2} {1}.", a, b, c);
string.Format("{0:d9}", someNumber);
// Refer to the documentation for specifics on this.
string v = @"This is a ""verbatim"" string. Backslashes, \, are not escaped.";
string p = $"This is an \"interpolated\" string. {myVariable | expression} !!";
// Only in .NET 4.6.


/*** CHECKING ***/

// You can use the "checked" keyword to carry out a checked cast, which checks
// for numeric overflow when narrowing a scope (e.g., int to byte).
checked
{
  byte a = (byte)someIntegerA;
  byte b = (byte)someIntegerB;
}
checked(byte a = (byte)someIntegerA);

// If you have enabled overflow checking project-wide, use "unchecked" to ignore blocks.
// The project-wide setting is usually in Build Settings > Check for arithmetic overflow/underflow.


/*** PARAMETER MODIFIERS ***/

// out - method MUST assign a value to the argument, not necessary to pre-initialize.
// ref - method MAY assign a value to the argument, necessary to pre-initialize.
// params - parameter list.

void Add(int a, int b, out int sum)
{
  sum = a + b;
}

void Swap(ref Card a, ref Card b)
{
  Card t = a;
  a = b;
  b = t;
}

// When called, out and ref params need to be specified again.
int sum;
Add(4, 5, out sum);

Card a = new Card("KD");
Card b = new Card("9D");
Swap(ref a, ref b);

// params allows a list of same-type objects to be given as parameters; e.g.,
int Sum(params int[] nums)
{
  int sum = 0;
  foreach (int i in nums)
  {
    sum += i;
  }
  return sum;
}
int result = Sum(1, 4, 3, 2);


/*** DEFAULT PARAMETERS ***/

void LogEvent(string msg, string tag = "Default", Color color = Color.blue) {}

// You can use "named parameters" to feed arguments out of order.
LogEvent(tag: "Timing");

// Unnamed, ordered parameters must come first though.
LogEvent(msg, color: Color.red);


/*** ARRAYS ***/

int[] a = {1, 2, 3}; // Simplest.
int[] a = new int[] {1, 2, 3};
int[] a = new int[3];

// Implicitly-typed.
var a = new[] {1, 2, 3};

// Matrix.
int[,] mat = new int[3, 4];
int[1, 1] = 5; // Assignment.

// Jagged array.
int[][] jag = new int[3][];
for (int i = 0; i < 4; ++i)
{
  jag[i] = new int[4];
}


/*** ENUMS ***/
enum MyEnum { YO }

enum MyEnum
{
  Three = 3, // Will start at 3.
  Four,
  Five
}

// By default, enums are System.Int32 (C# int).
// You can change that like this.
enum MyEnum : byte
{
  MyByte
}

// You can get the name as a string or raw value like this.
MyEnum.Three.ToString() // "Three"
(int)MyEnum.Three // 3


/*** VALUE TYPES ***/

// Structs cannot extend classes, or override the default constructor.

// [ValueTypes] are extended from object.
// ValueTypes declare their memory on the [stack] instead of the heap.
//  This means that a ValueType will be deallocated on exiting its scope in
//  which it was defined. In contrast to the managed heap where it would have
//  to wait to be garbage collected.

// Primitive types, enums, and structs are implemented using ValueTypes.

// ValueTypes are copied member-by-member when assigned to a variable or
//  passed as an argument. If a member is a ReferenceType (what classes use)
//  then only it's reference will be copied unless IClonable is implemented
//  by the ReferenceType.


// Side note (quirk with structs): If you have private fields and properties
// in a struct and a custom constructor, you need to call the default
// constructor in all custom constructors to initialize the fields with
// default values.
struct Point
{
  private int X { get; set; }
  private int Y { get; set; }
  private string label;

  public Point(string label) : this()
  { }
}

/*** NULLABLE ***/

// ValueTypes cannot be assigned null. They must be wrapped in Nullable<T>.
System.Nullable<int> i = null;

// There is a convenient shorthand for this, (?):
int? i = null;
bool? b = null;
float? t = 0.1f;

// The NULL COALESCING OPERATOR (??) assigns an alternate value if null.
int? a = null;
int b = a ?? 0; // If a is null, use 0.

Card cardA = null; // Can use ReferenceTypes too.
Card cardB = cardA ?? new Card("2C");

// The NULL CONDITIONAL OPERATOR (?.) provides safe method access.
Card cardA = deckA?.Get(1); // Get card at 1 if deckA is not null.
// Otherwise it will return null.

// You can make use of both above operators as such:
int length = arr?.Length ?? 0; // If array is not null, get length otherwise 0.


/*** CONSTRUCTOR CHAINING ***/

public Card()
  : this('2', 'C') {  }

public Card(char rank, char suit)
{
  // Master constructor, do stuff.
}

// Optional parameters can also be used in constructors above .NET 4.0.
public Card(char rank = '2', char suit = 'C')
{
  // Master constructor, do stuff.
}

// Call it like with a method with optional parameters.
Card cardA = new Card(suit: 'H'); // Uses default rank and sets suit.
Card cardB = new Card('A'); // Sets rank and uses default suit.


/*** STATIC CONSTRUCTOR ***/

// Initialize some static variables with a constructor.
static MyStaticConstructor()
{
  // No access modifiers allowed.
  // Called once on first instantiation of class or static member access.
}


/*** STATIC IMPORT ***/

// Imports static members.
using static ClassWithStaticMembers;


/*** ACCESS MODIFIERS ***/

public // Open to everyone.
private // Only members of the class.
protected // private + children
internal // within the assembly
protected internal // protected and within the assembly (no external children)

// Types are implicitly internal.
// Members are implicitly private.


/*** PROPERTIES ***/

// A basic .NET Property is as follows:
int someIntInAClass;
public int SomeInt
{
  get
  {
      return someIntInAClass;
  }
  set
  {
    someIntInAClass = value;
  }
}

// Static properties can be created also.


/*** AUTO-PROPERTIES ***/

public int SomeInt { get; set; }

// This uses a private field internally and sets that to a default value.
// For ReferenceTypes, the default value is null which can be a problem.
// Or the default value in general can not be what you want.
// In this case, use the constructor to assign a value.

// Alternatively, in C# 6.0 you can use a special technique:
public string SomeString { get; set; } = "My default string.";


/*** OBJECT INITIALIZATION SYNTAX ***/

// You can use object-literal notation in constructing an object.
Card c = new Card { Rank = '5', Suit = 'D' };
Card c = new Card() { Rank = '5', Suit = 'D' }; // Equivalent.
Card c = new Card(suit: 'D') { Rank = '5' }; // You can get crazy with this.
// The names in the brackets are public fields of the Card class,
//  not the names of the parameters in the constructor.


/*** READ-ONLY FIELDS ***/

// A const field must be assigned at the time of declaration.
// A readonly field can be assigned at declaration or in the constructor.

// const is implicitly static.
public const int PI = 3.1415f;

// readonly is an instance variable.
public readonly DateTime INITIALIZED;

// static can be used with readonly, which is almost like const
public static readonly DateTime FIRST_INITIALIZATION;

static MyBrand()
{ // The difference is you can use a static constructor to initialize.
  FIRST_INITIALIZATION = DateTime.Now;
}

public MyBrand()
{
  INITIALIZED = DateTime.Now;
}


/*** PARTIAL CLASSES ***/

// The 'partial' keyword allows classes to be split among multiple files.
//  The file names don't matter so long as the class name and namespace is the
//  same.

// Cookie.cs
partial class Cookie
{
  // Methods
  // Properties
}

// Cookie.Boilerplate.cs
partial class Cookie
{
  // Field data
  // Constructors
}


/*** SEALED CLASSES ***/

// This keyword prevents a class from being inherited.
sealed class MySealedClass {}


/*** THE BASE KEYWORD ***/

// Just like Java's "super", C# has the "base" keyword to call a parent ctor.
public MyClass(int a, string b)
  : base(a)
  {
    this.b = b;
  }

// You can also use "base" to access members of the base class.


/*** POLYMORPHISM ***/

// You can mark a method as overrideable with the "virtual" keyword.
public virtual void OverrideableMethod() {}

// In the child class:
public override void OverrideableMethod() {}

// You can call the parent method with:
base.OverrideableMethod();

// You can prevent a method from further being overriden by sealing it.
public override sealed void OverrideableMethod() {}


/*** ABSTRACT CLASSES ***/

abstract class MyAbstractClass
{
  public abstract void AbstractMethod();
}

class AnotherClass : MyAbstractClass
{
  public override void AbstractMethod() { /*...*/ }
}

/*** MEMBER SHADOWING ***/

// Instead of overriding, you can completely replace (shadow) the parent
// method. Shadowing can also be done with properties or other fields.
public new int MyIntProperty { get; set; }
public new void MyMethod() {}

// You can still access the parent's original member by explicit casting.
((Parent)child).MyMethod();


/*** CLASS CAST CHECKING ***/

// The AS keyword. (null if not type)
Deck d = someObject as Deck;
if (d != null) d.Shuffle();

// The IS keyword. (false if not type)
if (someObject is Deck)
{
  ((Deck)someObject).Shuffle();
}


/*** EXCEPTIONS ***/

// Fatal system exceptions extend SystemException and are thrown by the CLR.
// Extend ApplicationException, or just Exception, for app-related exceptions.

// You don't have to specify an exception in try-catch.
try {}
catch {}

// If you do the syntax is the same as Java.
try {}
catch (Exception e) {}

// You can rethrow an exception by just calling throw in the catch block.
try {}
catch
{
	// Stuff.
	throw;
}

// Throwing exceptions is the same as in Java too.
throw new SomeException();

// If an exception happens while handling another, the proper practice is to
// shove the inner exception in a new one of the parent type.
try {}
catch (CustomException ce)
{
	try { // Risky inner operation. }
	catch (Exception ie)
	{
		throw new CustomException(ce.Message, ie);
	}
}


/*** EXCEPTION FILTERS ***/

// Use "when" to conditionally run code in a catch block.
try {}
catch (Exception e) when (isDebuggingOn) {}


/*** INTERFACES ***/

// C# interfaces are similar to Java interfaces.

public interface IClashing
{
  public void ClashingMethod();
}

// If by chance you have methods in different interfaces that clash, you can
// define them explicitly.
class SomeClass : IClashing, IAlsoClashing
{
  public void IClashing.ClashingMethod() {}
  public void IAlsoClashing.ClashingMethod() {}
}

// You need to cast the object to the interface type to use these methods.
(someClass as IClashing)?.ClashingMethod();

IClashing ic = ((IClashing)someClass);
if (ic) ic.ClashingMethod();

// If you just define the clashing method alone it will override all of them.


/*** IENUMERABLE, IENUMERATOR, ITERATORS, and YIELD ***/

// Note: these examples don't use the generic versions, but use the generic
// versions to prevent penalities incurred by boxing.

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
  // This is an "iterator" method.
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

// You can use the iterators in a foreach loop:
foreach (Item it in someClass) {}
foreach (Item it in someClass.GetReverseEnumerator()) {}

// The "yield" keyword:

// When you use the yield keyword in a statement, you indicate that the method,
// operator, or 'get' accessor in which it appears is an iterator. Using yield
// to define an iterator removes the need for an explicit extra class (the class
// that holds the state for an enumeration, see IEnumerator<T> for an example)

// 'yield return' - return each element one at a time.
// 'yield break' - end the iteration. (Unclear exactly what this does,
// 	because the iterator will already stop when the method ends.)

// You consume an iterator method by using a foreach statement or LINQ query.
// Each iteration of the foreach loop calls the iterator method. When a
// 'yield return' statement is reached in the iterator method, the expression is
// returned, and the current location in code is retained. Execution is
// restarted from that location the next time that the iterator function is
// called.

// "yield" is not a reserved word and has special meaning only when
// it is used before a return or break keyword.

// An iterator method cannot have any ref or out parameters.

// An implicit conversion must exist from the expression type in the
// yield return statement to the return type of the iterator.


/*** CLONING (ICLONEABLE) ***/

// C#'s object class defines a protected MemberwiseClone() method that copies
// an object by copying the members (shallow copy).

// To perform a deep copy you can utilize ICloneable.
public interface ICloneable
{
  object Clone(); // Just create a new object and copy the values yourself.
}


/*** COMPARING ***/

// IComparable is notably used by Arrays.Sort.
public interface IComparable
{
  int CompareTo(object o); // As usual, -1 (lt), 0 (eq), 1 (gt).
}

// This is typically defined on a helper class,
//  (e.g., ISBNComparer.Compare(Book a, Book b)).
// Arrays.Sort also can use this: Arrays.Sort(books, new ISBNComparer()).
public interface IComparer
{
  int Compare(object a, object b);
}


/*** BOXING ***/

// Every time you assign a ValueType to a ReferenceType, e.g.,

  int f = 5;
  object oa = f;

// C# does a 'box' operation: allocates an object on the heap and
// moves the value from the stack to the new object.

// C# does an 'unbox' operation to do the inverse, and requires casting:

  int g = (int)oa;

// This is very inefficient speed-wise and memory-wise.


/*** GENERICS ***/

// Generics provide better performance because they do not result in boxing
// or unboxing penalities when storing value types.

// Generic Method.
void AddToList<T>(T item) {
  Console.Log("Adding item of type " + typeof(T));
}

public struct Point<T>
{
  pulbic void ResetPoint()
  {
    x = default(T); // Sets the default value for the (generic) type.
    y = default(T);
  }
}

// Generic Constraints

public class MyGenericClass<T> where T : class, IDrawable, new()
{ }

// Possible Constraints

where T : struct // ValueType
where T : class // Not ValueType
where T : new() // Must have a default ctor. Must be last in the list.
where T : NameOfBaseClass
where T : NameOfInterface // Can have mutliple interfaces.
where T : U // Depends on U (if it takes another generic param. U)

// Constraining multiple parameters

class MyGeneric<T, U>
	where T : Set<U>
	where U : struct
	{ }

/*** THE System.Collection.Generics NAMESPACE ***/

// Generic Interfaces

ICollection<T>  // Defines general characteristics (e.g., size, enumeration,
// and thread saftey) for all generic collection types.
IComparer<T>
IDictionary<T> // Alows a generic collection object to represent its contents
// using key-value pairs.
IEnumerable<T>
IEnumerator<T>
IList<T>
ISet<T>

// Generic Collections

Dictionary<TKey, TValue>
LinkedList<T>
List<T>
Queue<T>
SortedDictionary<TKey, TValue>
SortedSet<T>
Stack<T>


/*** THE System.Collection.ObjectModel NAMESPACE ***/

// This contains a couple important collection objects that notify listeners
// when the collection is modified.

ObservableCollection<T>
ReadOnlyObservableCollection<T>


/*** DELEGATES ***/

// Declaring a delegate type.
public delegate string MyDelegate(bool b, int n);

// Declaring an initializing a delegate variable.
MyDelegate mdel = new MyDelegate(SomeMethod);

// Invoking the delegate.
mdel();

// With "method group conversion syntax" just assign the method name.
MyDelegate mdel = SomeMethod;

// Delegates can be combined to call multiple methods.
mdel += AnotherMethod;

// The compiler generates a class for a delegate.
sealed class MyDelegate : System.MulticastDelegate
{
	// Basic synchronous invocation.
	public string Invoke(bool b, int n);

	// The other two are for asynchronous use.
	public IAsyncResult BeginInvoke(bool b, int b, AsyncCallback cb, object state);
	public string EndInvoke(IAsyncResult result);
}

// The MulticastDelegate class is defined as such:
public abstract class MulticastDelegate : delegate
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

// The Delegate class is defined as such:
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

// * The '+=' operator is shorthand for the Combine() method.
// * The '-=' operator is shorthand for the Remove() method.
// * 'Method' gets target method details.
// * 'Target' gets object details if the target method belongs to an instance.


// GENERIC DELEGATES

public delegate void GenericDel<T>(T arg);

public void MyFunc(string a) { }
GenericDel<string> strTarget = MyFunc;


// THE GENERIC ACTION<> AND FUNC<> DELEGATES

// These two are out-of-the-box, generic delegate types for simplifying
// the definition of a delegate.

// Action<> takes up to 16 arguments.
// Action<> can only point to functions with a void return type.

static void DisplayMessage(string msg, ConsoleColor col) { }
Action<string, ConsoleColor> target = DisplayMessage;
target("hello" ConsoleColor.Blue);

// Func<> takes up to 16 arguments.
// The last generic parameter is the return type.

static string IntToString(int a) { }
Func<int, string> target = IntToString;
string s = target(5);

// PREDICATE<T>

// Defined in System as such:
public delegate bool Predicate<T>(T obj);

// This is used in the List.FindAll() method.


/*** EVENT KEYWORD ***/

// The event keyword automatically generates registration methods and
// handles the list of methods for a given delegate type.

public delegate string MyDelegate(int a);

// Declaring the event type.
public event MyDelegate MyEvent;

// Invoking the event.
MyEvent(5);

// In external code; adding a method.
MyClass.MyEvent += SomeMethod;

// Using events is preferred over using a raw delegate variable and
// manually written registration methods.

// Registration methods for example can be Register() and UnRegister().


/// EVENTARGS

// Microsoft's recommended pattern for events is to pass the invoking
// object and related data in an EventArgs instance.

public delegate void MyDelegate(object sender, MyEventArgs e);

// The default EventArgs class is just empty.
public class EventArgs
{
  public static readonly EventArgs Empty;
  public EventArgs();
}

// Custom EventArgs class.
public class MyEventArgs : EventArgs
{
  public readonly string msg;
  public MyEventArgs(string msg) { this.msg = msg; }
}

// In some code.
MyEvent(this, new MyEventArgs("Woah, custom EventArgs!"));


// EVENTHANDLER<T>

// Given the recommended (object sender, EventArgs e) pattern, there is a
// generic event type to streamline everything.

public event EventHandler<MyEventArgs> MyEvent;

// With EventHandler<T> you don't need to define the delegate, only the
// custom EventArgs class.


/*** ANONYMOUS METHODS ***/

someDelegateOrEvent += delegate
{
  Console.WriteLine("Anonymous method, ignores arguments.");
};

someDelegateOrEvent += delegate(int a, string b)
{
  Console.WriteLine("Anonymous method, accepts arguments.");
};

// * An anonymous method cannot access ref or out params in outer method.
// * An anonymous method cannot have local variable same as in outer method.
// * Stuff in the outer class scope behaves as usual though.


/*** LAMBDA EXPRESSIONS ***/

() => Console.WriteLine("Hello");

i => (i % 2) == 0

(i) => (i % 2) == 0

(int i) => (i % 2) == 0

(a, b) => a + b

(a) => {
  Console.WriteLine(a);
  return a + 1;
}

// As of .NET 4.6, you can do lambdas for member methods too!

class MyClass {
  public int Add(int x, int y) => x + y
}


/*** INDEXER METHODS ***/

public ReturnType this[int i] {
  get { return (ReturnType)internalList[i]; }
  set { internalList[i] = value; }
}

public ReturnType this[string s] {
  /* ... */
}

// Multidimensional indexing.
public ReturnType this[string a, string b] {
  /* ... */
}

// These can be overloaded so you can use any combination of indexing
// methods in your custom class.

// You can also define them in an interface.
public interface IMyContainer {
  string this[int index] { get; set; }
}


/*** OPERATOR OVERLOADING ***/

// Various operators in C# can be overloaded.

public static Point operator + (Point p1, Point p2) {
  return new Point(p1.x + p2.x, p1.y + p2.y);
}

// With binary operators, you get the corresponding assignment operator.
// In the above example you also get '+='.

// Both operands don't have to be the same type.
public static Point operator * (Point p1, float scalar) {
  return new Point(p1.x * scalar, p1.y * scalar);
}

// If you do this though, do it both ways to support commutativity.
public static Point operator * (float scalar, Point p1) {
  return new Point(p1.x * scalar, p1.y * scalar);
}

// You can override some unary operators, such as ++.
public static Point operator ++ (Point p) {
  return new Point(p.x + 1, p.y + 1);
}

// In C++ you can override the pre/post increment/decrement operators
// seperately. In C# you can't, but you get the correct behavior for free.


/*** CONVERSION METHODS ***/

// Explicit.
public static explicit operator Square(Rectangle r) {
  return new Square(r.Height);
}

// You can now explicitly cast from Rectangle to Square.
Rectangle myRectangle = new Rectangle(4, 4);
Square mySquare = (Square)myRectangle;

// Implicit.
public static implicit operator Rectangle(Square s) {
  return new Rectangle(s.Length, s.Length);
}

// You can not implicitly from Square to Rectangle.
Square mySquare =  new Square(4);
Rectangle myRectangle = mySquare;

// In the implicit case, you also get the explicit cast for free.
// This makes sense because if it happens automatically, you should also
// be allowed to be verbose about it.
Rectangle myRectangle2 = (Rectangle)mySquare;

// Conversion methods can be defined in structs as well.


/*** EXTENSION METHODS ***/

using System.Reflection;

static class MyExtensions
{
  // Targeting the Rectangle type.
  public static bool IsSquare(this Rectangle r)
  {
    return r.Height == r.Width;
  }

  // Targeting an int type.
  public static int Add(this int a, int b)
  {
    return a + b;
  }

  // You can also define extension methods targeting interfaces!
  public static void PrintAll(this System.Collections.IEnumerable iterator)
  {
    foreach (var item in iterator)
    {
      Console.WriteLine(item);
    }
  }
}

// Extension methods must be defined in a static class, and must be static.
// These add methods to existing types.
// The target type is specified as the first parameter.
// Additional parameters are allowed.

int myInt = 30;
int result = myInt.Add(2); // Using the extension method from MyExtensions.


/*** ANONYMOUS TYPES ***/

// Defines an anonymous type (class) with readonly properties.
var anon = new { Title = "Hello", Author = "Unknown" };

// * Anonymous types override ToString(), GetHashCode(), and Equals() to
//   work with value-based equality.
// * The == operator compares by reference though.


/*** POINTER TYPES ***/

// Though rarely used in C# developement, you can actually use pointers.
// To use you need to:
// * Define the /unsafe flag on compilation: 'csc /unsafe *.cs'
// * Check 'Allow unsafe code' in Visual Studio.
// * Use the unsafe keyword for blocks and methods.

// 'unsafe'

class Program
{
  static void Main(string[] args)
  {
    unsafe
    {
      int myInt = 5;
      Square(&myInt);
    }
  }

  unsafe static void Square(int *n)
  {
    return *n *= *n;
  }
}

unsafe struct Node
{
  // Entire struct is unsafe/
}

struct Node
{
  public int Value;
  public unsafe Node* next; // Unsafe field.
}

// Unlike in C and C++, the * operator must be next to the type for pointer
// declaration, e.g.,
  public unsafe Node* left, right; // C#
// instead of,
  public Node *left, *right; // C, C++

// 'stackalloc'

char* p = stackalloc char[256]; // Declare memory on the stack.

// 'fixed'

// To prevent a ReferenceType from being sweeped or moved by the GC while
// you are using it's address, use the 'fixed' keyword and scope.

fixed (MyRefType* p = &refVar)
{
  Console.Write(p->ToString());
}
