
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

// Entire struct is unsafe.
unsafe struct Node
{ /* ... */ }

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

// 'sizeof'
Console.Write(sizeof(int));

// Must be in 'unsafe' block for custom types.
unsafe { Console.Write(sizeof(Point)); }


/*** LINQ (to Objects)***/

// * A query language built with C# that allows unified data access to various
//   types of data.
// * LINQ (Language Integrated Query) appears similar to SQL.
// * Based on what data is being targeted, people use different phrases:
//   * LINQ to Objects (arrays and collections)
//   * LINQ to XML
//   * LINQ to DataSet (ADO.NET type)
//   * LINQ to Entities (ADO.NET Entity Framework [EF])
//   * Parallel LINQ (aka PLINQ) (parallel processing of data)
// * Available in .NET 3.5 and up.
// * Must include 'System.Linq' for core LINQ usage (in System.Core.dll).

// Example
string[] games = {"Morrowind", "Uncharted 2", "Fallout 3", "Daxter"};
IEnumerable<string> subset =
  from game in games
  where game.Contains(" ")
  orderby game
  select game;

// Cleaner (and recommended) is to use implicit typing.
var subset = from game in games where game.C//...

// * LINQ queries return various types which implement IEnumerator<T>.
// * Deferred execution: LINQ results are not evaluated until you actually
//   iterate over the sequence.
// * Immediate execution: You can return a snapshot of the result sequence
//   with extension methods provided by LINQ. Some are: ToArray<T>(),
//   ToDictionary<TSource, TKey>(), and ToList<T>().

// If you have a nongeneric collection (like ArrayList) you can use OfType
// to convert (and filter) it to a generic collection.
ArrayList myList = new ArrayList();
myList.AddRange(new object[] { 4, "g", new Pineapple(), 88 });
var myListG = myList.OfType<int>(); // Make typed array of ints from myList.

// More expression examples:
var res = from n in numList select n; // Most basic.

// Using many of the operators.
var res = from n in numList where n>0 && n<10 orderby n descending select n;

// Projection: return a subset type. In this case as an anonymous type.
var res = from book in bookList select new {book.Title, book.Author};

// * Other operators are (join, on, equals, into, group, by).
// * There's also aggregation and set extension methods too:
//   * Count(), Reverse(), Intersect(otherRes),
//     Union(otherRes), Concat(otherRes), Distinct(),
//     Max(), Min(), Average(), Sum().
// * LINQ queries are shorthand for a bunch of extension methods (e.g.
//   Where(Func<T> fn), Select(Func<T> fn), etc).


/*** OBJECT LIFETIME STUFF ***/

// * Finalize() is called when an object is garbage collected.
//   * In C# you define this similar to a C++ destrutor.
//   * This is used only in rare circumstances.
// * IDisposable defines the Dispose().
//   * Called by the user to manually release resources.
// * Lazy<T> is a wrapper class that implements lazy initialization.

/*** Attributes ***/

[Serializable]
public class MyClass
{
  private int myField;

  [NotSerialized]
  private string myBrand;
}

// C# Attribute shorthand.
// By convention, attribute classes are suffixed with 'Attribute'. For example,
// 'SerializableAttribute'. However, C# allows you to leave out the word
// 'Attribute'.
