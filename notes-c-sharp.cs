








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
