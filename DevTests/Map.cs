using System;
using System.Collections.Generic;

namespace DevTests
{
    /// <summary>
    /// A key-value store
    /// Uses a binary tree to efficiently store items in-order
    /// </summary>
    /// <typeparam name="K">Key type</typeparam>
    /// <typeparam name="V">Value type</typeparam>
    public class Map<K, V>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Map{K, V}" /> class
        /// </summary>
        public Map()
        {
        }

        /// <summary>
        /// Gets or sets value to related to a key in the map
        /// </summary>
        /// <param name="key">The key to get or set</param>
        public V this[K key]
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
        /// Gets the count of itmes in the map
        /// </summary>
        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets a collection of the keys in the map
        /// </summary>
        public IEnumerable<K> Keys
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets a collection of the value in the map
        /// </summary>
        public IEnumerable<V> Values
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Add a key-value pair to the map
        /// </summary>
        /// <param name="item">A tuple containing the key and value</param>
        public void Add(Tuple<K, V> item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a key-value pair to the map
        /// </summary>
        /// <param name="key">The key to add</param>
        /// <param name="value">The value to be stored against the key</param>
        public void Add(K key, V value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes all items from the map
        /// </summary>
        public void Clear()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if value is present in the map
        /// </summary>
        /// <param name="value">The value to search for</param>
        /// <returns>True if the value exists in the map, False otherwise</returns>
        public bool ContainsValue(V value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if key is present in the map
        /// </summary>
        /// <param name="key">The key to search for</param>
        /// <returns>True if the key exists in the map, False otherwise</returns>
        public bool ContainsKey(K key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes an item from the map by key
        /// </summary>
        /// <param name="key">The key of the item to remove</param>
        /// <returns>True if the key was found and removed, False otherwise</returns>
        public bool Remove(K key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tries to get a value from the map by key
        /// </summary>
        /// <param name="key">The key of the item to find</param>
        /// <returns>An optional result</returns>
        public Option<V> TryGetValue(K key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Streams the items of the map in-order as a series of key-value tuples
        /// </summary>
        /// <returns>Enumerable of tuples</returns>
        public IEnumerable<Tuple<K, V>> AsEnumerable()
        {
            throw new NotImplementedException();
        }

        class MapItem
        {
            public K Key;
            public V Value;
            public MapItem Left;
            public MapItem Right;

            public MapItem(K key, V value, MapItem left, MapItem right)
            {
                Value = value;
                Left = left;
                Right = right;
            }
        }        
    }
}
