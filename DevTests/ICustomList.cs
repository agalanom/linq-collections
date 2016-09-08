using System;
using System.Collections.Generic;

namespace DevTests
{
    /// <summary>
    /// Interface for common list methods
    /// </summary>
    /// <typeparam name="T">Value type</typeparam>
    public interface ICustomList<T>
    {
        void Add(T value);

        IEnumerable<T> AsEnumerable();

        void Clear();

        bool Contains(T item);

        int Count { get; }

        IEnumerator<T> GetEnumerator();

        int IndexOf(T item);

        void Insert(int index, T item);

        bool Remove(T item);

        void RemoveAt(int index);

        T this[int index] { get; set; }
    }
}
