





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
