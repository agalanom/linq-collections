using System;
using System.Collections.Generic;

namespace DevTests
{
    /// <summary>
    /// A doubly-linked list
    /// Stores a list of items in the order they are added
    /// </summary>
    /// <typeparam name="T">Value type</typeparam>
    public class DoubleList<T> : ICustomList<T>
    {
        ListItem head;
        ListItem last;

        int size;

        /// <summary>
        /// Initializes a new instance of <see cref="DoubleList{T}"/> class
        /// </summary>
        public DoubleList()
        {
            head = last = null;
            size = 0;
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
        /// Gets or sets an item located on the specified position (index)
        /// </summary>
        /// <param name="index">New item index</param>
        /// <returns>The item located on the specified position</returns>
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
        /// Adds an item to the end of the list
        /// </summary>
        /// <param name="value">The item to append</param>
        public void Add(T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (last == null)
            {
                if (head == null)
                {
                    // The list is empty, insert in the beginning
                    head = last = new ListItem(null, null, value);
                }
            }
            else
            {
                // Insert to the end and point the previous to the old last item
                last.Next = new ListItem(null, last, value);
                // Set the new last item
                last = last.Next;
            }

            size++;
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

            int currentIndex;
            ListItem currentItem;

            // Check if the index is closer to the start or the end of the list
            if (index < size / 2)
            {
                // Start from the beginning
                currentIndex = 0;
                currentItem = head;

                while (currentItem != null)
                {
                    if (currentItem.Value.Equals(item))
                    {
                        // Found the item
                        index = currentIndex;
                        break;
                    }
                    // Keep going down the list
                    currentIndex++;
                    currentItem = currentItem.Next;
                }
            }
            else
            {
                // Start from the end
                currentIndex = size - 1;
                currentItem = last;

                while (currentItem != null)
                {
                    if (currentItem.Value.Equals(item))
                    {
                        // Found the item
                        index = currentIndex;
                        break;
                    }
                    // Keep going down the list
                    currentIndex--;
                    currentItem = currentItem.Previous;
                }
            }

            return index;
        }

        /// <summary>
        /// <summary>
        /// Insert an item to a specified position
        /// </summary>
        /// <param name="index">The index position to insert to</param>
        /// <param name="item">The item to insert</param>
        public void Insert(int index, T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var currentItem = FindItemAt(index);

            if (currentItem.Previous == null)
            {
                // There is no previous item i.e. this is the head
                // Insert before the head and point the next to the old head
                head.Previous = new ListItem(head, null, item);
                // Set the new head item
                head = head.Previous;
            }
            else
            {
                // Set the next to the next item and the previous to the current item
                //var newItem = new ListItem(currentItem.Next, currentItem, item);
                var newItem = new ListItem(currentItem, currentItem.Previous, item);
                /*
                if (currentItem.Next == null)
                {
                    // There is no next item i.e. this is the last item, so set the new last item
                    last = newItem;
                }
                else*/
                {
                    currentItem.Previous.Next = newItem;
                }
                currentItem.Previous = newItem;
                    /*
                    // Point the previous item in the list to the new item
                    currentItem.Next.Previous = newItem;
                }
                // Point the next item in the list to the new item
                currentItem.Next = newItem;*/
            }
            size++;
        }

        /// <summary>
        /// Remove item located at the specified position (index)
        /// </summary>
        /// <param name="index">The index</param>
        public void RemoveAt(int index)
        {
            var currentItem = FindItemAt(index);

            if (currentItem.Previous == null)
            {
                // There is no previous node i.e. the head was removed so set a new head
                head = currentItem.Next;
            }
            else
            {
                // Point the previous item in the list to the item after the deleted one
                currentItem.Previous.Next = currentItem.Next;
            }

            if (currentItem.Next == null)
            {
                // There is no next node i.e. the last item was removed so set a new last item
                last = currentItem.Previous;
            }
            else
            {
                // Point the next item in the list to the item before the deleted one
                currentItem.Next.Previous = currentItem.Previous;
            }

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

            int currentIndex;
            ListItem currentItem;

            // Check if the index is closer to the start or the end of the list
            if (index < size / 2)
            {
                // Start from the beginning
                currentIndex = 0;
                currentItem = head;

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
            }
            else
            {
                // Start from the end
                currentIndex = size - 1;
                currentItem = last;

                while (currentItem != null)
                {
                    if (currentIndex == index)
                    {
                        // Found the item
                        break;
                    }
                    // Keep going down the list
                    currentIndex--;
                    currentItem = currentItem.Previous;
                }
            }
            return currentItem;
        }

        class ListItem
        {
            public ListItem Next;
            public ListItem Previous;
            public T Value;

            public ListItem(ListItem next, ListItem previous, T value)
            {
                this.Next = next;
                this.Previous = previous;
                this.Value = value;
            }
        }
    }
}
