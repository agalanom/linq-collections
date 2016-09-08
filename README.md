# C# Collections & LINQ

Complete the implementation of the following types:

### SingleList

This is a singly-linked list. The `Add` method is already implemented. Fill in the rest of the implementation.

Do not derive from `IEnumerable<T>`, `IComparable<T>` or `IEquatable<T>` and do not create an `IEnumerator<T>` derived enumerator class for `GetEnumerator()`.

### DoubleList

This is a doubly-linked list. Nothing has been implemented. Fill in the rest of the implementation.

Do not derive from `IEnumerable<T>`, `IComparable<T>` or `IEquatable<T>` and do not create an `IEnumerator<T>` derived enumerator class for `GetEnumerator()`.

### Map

This is a dictionary type: a key-value store. Items added are stored in-order using a binary tree.

Do not derive from `IDictionary<K,V>`, `IEnumerable<T>`, `IComparable<T>` or `IEquatable<T>`.

Additional points for:
* making the tree balanced.
* making the Map structure immutable.


## LINQ Extensions

There is a static class `Extensions` in `Extensions.cs` - implement some basic LINQ operators for the types listed above.

* Implement Select, SelectMany and Where extension methods for DoubleList and SingleList  
* Implement Select and Where extension methods for Map
