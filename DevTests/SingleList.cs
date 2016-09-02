using System;
using System.Collections.Generic;

namespace DevTests
{
    /// <summary>
    /// A singly-linked list
    /// Stores a list of items in the order they are added
    /// </summary>
    /// <typeparam name="T">Value type</typeparam>
    public class SingleList<T>
    {
        ListItem head;
        ListItem last;

        public SingleList()
        {
            head = new ListItem(null, default(T));
            last = head;
        }

        /// <summary>
        /// Access an item in the list by index
        /// </summary>
        /// <param name="index">Index of the item in the list</param>
        /// <returns>The item found</returns>
        public T this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the number of items in the list
        /// </summary>
        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Adds an item to the end of the list
        /// </summary>
        /// <param name="value">The item to append</param>
        public void Add(T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            var item = new ListItem(null, value);
            last = last.Next = item;
        }

        /// <summary>
        /// Returns the index of the first occurence of an item
        /// </summary>
        /// <param name="item">The item to find the index of</param>
        /// <returns>The index of the item, -1 if not found</returns>
        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Insert an item to a specified position
        /// </summary>
        /// <param name="index">The index position to insert to</param>
        /// <param name="item">The item to insert</param>
        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove item located at the specified position (index)
        /// </summary>
        /// <param name="index">The index</param>
        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears all items from the list
        /// </summary>
        public void Clear()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if an item is contained in the list
        /// </summary>
        /// <param name="item">The item</param>
        /// <returns>True if the item is in the list</returns>
        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes item from the list
        /// </summary>
        /// <param name="item">The item</param>
        /// <returns>True if the item was found and removed, False otherwise</returns>
        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return an enumerator for the collection
        /// </summary>
        /// <returns>Enumerator of T</returns>
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the list as an enumerable
        /// </summary>
        /// <returns>Enumerable of single list items</returns>
        public IEnumerable<T> AsEnumerable()
        {
            throw new NotImplementedException();
        }

        class ListItem
        {
            public ListItem Next;
            public T Value;

            public ListItem(ListItem next, T value)
            {
                Next = next;
                Value = value;
            }
        }
    }
}
