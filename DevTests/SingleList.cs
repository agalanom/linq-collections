using System;
using System.Collections.Generic;

namespace DevTests
{
    /// <summary>
    /// A singly-linked list
    /// Stores a list of items in the order they are added
    /// </summary>
    /// <typeparam name="T">Value type</typeparam>
    public class SingleList<T> : ICustomList<T>
    {
        ListItem head;
        ListItem last;

        int size;

        public SingleList()
        {
            head = last = null;
            size = 0;
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
                return FindItemAt(index).Value;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));

                FindItemAt(index).Value = value;
            }
        }

        /// <summary>
        /// Gets the number of items in the list
        /// </summary>
        public int Count
        {
            get
            {
                return size;
            }
        }

        /// <summary>
        /// Adds an item to the end of the list
        /// </summary>
        /// <param name="value">The item to append</param>
        public void Add(T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            size++;

            var item = new ListItem(null, value);
            
            if (head == null)
            {
                // This is the first node, so make it the head
                head = item;
            }
            else
            {
                // This is not the head, make it the next item
                last.Next = item;
            }

            // Set the item just added to be the last one
            last = item;
            //last = last.Next = item;
        }

        /// <summary>
        /// Returns the index of the first occurence of an item
        /// </summary>
        /// <param name="item">The item to find the index of</param>
        /// <returns>The index of the item, -1 if not found</returns>
        public int IndexOf(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            // Index is -1 if not found
            int index = -1;
            // Start from the head of the list
            var currentIndex = 0;
            var currentItem = head;

            while (currentItem != null)
            {
                if (currentItem.Value.Equals(item))
                {
                    // Found the item, exit the loop
                    index = currentIndex;
                    break;
                }
                // Keep going down the list
                currentItem = currentItem.Next;
                currentIndex++;
            }

            return index;
        }

        /// <summary>
        /// Insert an item to a specified position
        /// </summary>
        /// <param name="index">The index position to insert to</param>
        /// <param name="item">The item to insert</param>
        public void Insert(int index, T item)
        {
            if (index < 0 || index > size - 1) throw new IndexOutOfRangeException();
            if (item == null) throw new ArgumentNullException(nameof(item));

            if (index == 0)
            {
                // The list is not empty, set the new head
                head = new ListItem(head, item);
            }
            else
            {
                // Find the item before the one we want
                var currentItem = FindItemAt(index - 1);
                // Add the new one as next
                currentItem.Next = new ListItem(currentItem.Next, item);
            }
            // Increment size
            size++;
        }

        /// <summary>
        /// Remove item located at the specified position (index)
        /// </summary>
        /// <param name="index">The index</param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index > size - 1) throw new IndexOutOfRangeException();

            if (head == last)
            {
                // There is only one item, clear both head and last
                head = last = null;
            }
            else if (index == 0)
            {
                // The item to be removed is the head, set it to the next item
                head = head.Next;
            }
            else
            {
                // Find the item before the one we want
                var currentItem = FindItemAt(index - 1);
                // Set the next item to the one after that
                currentItem.Next = currentItem.Next.Next;
                // If deleting the last item, also set the last item
                if (currentItem.Next == null) last = currentItem;
            }
            // Decrement size
            size--;
        }

        /// <summary>
        /// Clears all items from the list
        /// </summary>
        public void Clear()
        {
            // Clear the head and the last node
            head = last = null;

            // Set the list size to zero
            size = 0;
        }

        /// <summary>
        /// Checks if an item is contained in the list
        /// </summary>
        /// <param name="item">The item</param>
        /// <returns>True if the item is in the list</returns>
        public bool Contains(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            // Get the index of the item, if positive then the item is in the list
            return IndexOf(item) >= 0;
        }

        /// <summary>
        /// Removes item from the list
        /// </summary>
        /// <param name="item">The item</param>
        /// <returns>True if the item was found and removed, False otherwise</returns>
        public bool Remove(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            // Get the index of the item
            int index = IndexOf(item);
            if (index != -1)
            {
                // The item is found, remove it
                RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Return an enumerator for the collection
        /// </summary>
        /// <returns>Enumerator of T</returns>
        public IEnumerator<T> GetEnumerator()
        {
            // Start from the beginning
            ListItem currentItem = head;
            while (currentItem != null)
            {
                yield return currentItem.Value;
                // Keep going down the list
                currentItem = currentItem.Next;
            }
        }

        /// <summary>
        /// Get the list as an enumerable
        /// </summary>
        /// <returns>Enumerable of single list items</returns>
        public IEnumerable<T> AsEnumerable()
        {
            // Start from the beginning
            ListItem currentItem = head;
            while (currentItem != null)
            {
                yield return currentItem.Value;
                // Keep going down the list
                currentItem = currentItem.Next;
            }
        }

        /// <summary>
        /// Find a <see cref="ListItem"/> by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The <see cref="ListItem"/> located on the specified position</returns>
        /// <exception cref="IndexOutOfRangeException">when index is out of range.</exception>
        private ListItem FindItemAt(int index)
        {
            if (index < 0 || index > size - 1) throw new IndexOutOfRangeException();

            var currentIndex = 0;
            var currentItem = head;

            while (currentItem != null)
            {
                if (currentIndex == index)
                {
                    // Found the item
                    break;
                }
                // Keep going down the list
                currentIndex++;
                currentItem = currentItem.Next;
            }

            return currentItem;
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
