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
        MapItem root;

        int size;

        /// <summary>
        /// Initializes a new instance of <see cref="Map{K, V}" /> class
        /// </summary>
        public Map()
        {
            root = null;
            size = 0;  
        }

        /// <summary>
        /// Gets or sets value to related to a key in the map
        /// </summary>
        /// <param name="key">The key to get or set</param>
        public V this[K key]
        {
            get
            {
                var item = FindItem(key);
                if (item == null) throw new KeyNotFoundException();

                return item.Value;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));

                var item = FindItem(key);
                if (item == null) throw new KeyNotFoundException();

                item.Value = value;
            }
        }

        /// <summary>
        /// Gets the count of items in the map
        /// </summary>
        public int Count
        {
            get
            {
                return size;
            }
        }

        /// <summary>
        /// Gets a collection of the keys in the map
        /// </summary>
        public IEnumerable<K> Keys
        {
            get
            {
                // Store the tree nodes in a stack
                var stack = new Stack<MapItem>();
                // Add the root node to the stack
                stack.Push(root);
                // Keep repeating while there are nodes in the stack
                while (stack.Count > 0)
                {
                    // Get the last item in the stack
                    var next = stack.Pop();
                    if (next != null)
                    {
                        // Return the key of the node
                        yield return next.Key;
                        // Push the node's children in the stack
                        stack.Push(next.Left);
                        stack.Push(next.Right);
                    }
                }
            }
        }

        /// <summary>
        /// Gets a collection of the value in the map
        /// </summary>
        public IEnumerable<V> Values
        {
            get
            {
                // Store the tree nodes in a stack
                var stack = new Stack<MapItem>();
                // Add the root node to the stack
                stack.Push(root);
                // Keep repeating while there are nodes in the stack
                while (stack.Count > 0)
                {
                    // Get the last item in the stack
                    var next = stack.Pop();
                    if (next != null)
                    {
                        // Return the key of the node
                        yield return next.Value;
                        // Push the node's children in the stack
                        stack.Push(next.Left);
                        stack.Push(next.Right);
                    }
                }
            }
        }

        /// <summary>
        /// Add a key-value pair to the map
        /// </summary>
        /// <param name="item">A tuple containing the key and value</param>
        public void Add(Tuple<K, V> item)
        {
            Add(item.Item1, item.Item2);
        }

        /// <summary>
        /// Add a key-value pair to the map
        /// </summary>
        /// <param name="key">The key to add</param>
        /// <param name="value">The value to be stored against the key</param>
        public void Add(K key, V value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            var newItem = new MapItem(key, value);
            if (root == null)
            {
                // The tree is empty, add as the root
                root = newItem;
            }
            else
            {
                // Insert in the first empty position
                Insert(ref root, newItem);
            }

            size++;
        }

        /// <summary>
        /// Removes all items from the map
        /// </summary>
        public void Clear()
        {
            root = null;
            size = 0;
        }

        /// <summary>
        /// Checks if value is present in the map
        /// </summary>
        /// <param name="value">The value to search for</param>
        /// <returns>True if the value exists in the map, False otherwise</returns>
        public bool ContainsValue(V value)
        {
            // Iterate through all the values
            foreach (V itemValue in Values)
            {
                // Return true if the value is found
                if (Equals(itemValue, value)) return true;
            }
            // The value was not found, return false
            return false;
        }

        /// <summary>
        /// Checks if key is present in the map
        /// </summary>
        /// <param name="key">The key to search for</param>
        /// <returns>True if the key exists in the map, False otherwise</returns>
        public bool ContainsKey(K key)
        {
            var item = FindItem(key);

            return (item != null);
        }

        /// <summary>
        /// Removes an item from the map by key
        /// </summary>
        /// <param name="key">The key of the item to remove</param>
        /// <returns>True if the key was found and removed, False otherwise</returns>
        public bool Remove(K key)
        {
            var item = FindItem(key);
            if (item != null)
            {
                // Get the parent
                var parent = FindParent(item);

                // It's a leaf node with no children
                if ((item.Left == null) && (item.Right == null))
                {
                    // Remove by making it null
                    // Special case for root node
                    if (parent == null)
                        root = null;
                    else if (item == parent.Left)
                        parent.Left = null;
                    else
                        parent.Right = null;
                    size--;
                }
                // it has either a left or right child
                else if ((item.Left == null) || (item.Right == null))
                {
                    // Get the child
                    var child = item.Left == null ? item.Right : item.Left;

                    // Special case for root node
                    if (parent == null)
                        root = child;
                    // Make parent point to it's child
                    else if (item == parent.Left)
                        parent.Left = child;
                    else
                        parent.Right = child;
                    size--;
                }
                else
                {
                    // Find the nodes successor and delete it
                    var successor = FindSuccessor(item);
                    Remove(successor.Key);
                    // Maintain the leafs of the item to be deleted
                    successor.Left = item.Left;
                    successor.Right = item.Right;

                    // Special case for root node
                    if (item == root)
                        root = successor;
                    else if (parent.Left == item)
                    {
                        // Set the new parent of the node
                        parent.Left = successor;
                    }
                    else
                    {
                        // Set the new parent of the node
                        parent.Right = successor;
                    }
                }
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Find the parent of an item
        /// </summary>
        /// <param name="item">The <cref="MapItem"/> node to find the parent of</param>
        /// <returns></returns>
        private MapItem FindParent(MapItem item)
        {
            MapItem currentItem = root;
            MapItem parent = null;
            int cmp;
            while (currentItem != null)
            {
                // Compare the keys, need to use the comparer default method
                cmp = Comparer<K>.Default.Compare(item.Key, currentItem.Key);
                if (cmp == 0)
                {
                    // Found
                    return parent;
                }

                if (cmp < 0)
                {
                    parent = currentItem;
                    // Item is smaller than this one, keep going left
                    currentItem = currentItem.Left;
                }
                else
                {
                    parent = currentItem;
                    // Else keep going right
                    currentItem = currentItem.Right;
                }
            }
            // Not found, return null
            return null;
        }

        /// <summary>
        /// Find the successor of the item
        /// </summary>
        /// <param name="item">The <cref="MapItem"/> node to find the successor of</param>
        /// <returns></returns>
        private MapItem FindSuccessor(MapItem item)
        {
            // Find the minimum node in the right subtree
            return FindMin(item.Right);
        }

        /// <summary>
        /// Finds the minimum node in a subtree
        /// </summary>
        /// <param name="start">The <cref="MapItem"/> to start from</param>
        /// <returns></returns>
        private MapItem FindMin(MapItem start)
        { 
            // Keep going to the left until there are no more left children
            while (start.Left != null)
            {
                start = start.Left;
            }
            // Found the smallest value
            return start;
        }

        /// <summary>
        /// Tries to get a value from the map by key
        /// </summary>
        /// <param name="key">The key of the item to find</param>
        /// <returns>An optional result</returns>
        public Option<V> TryGetValue(K key)
        {
            var item = FindItem(key);

            return (item == null) ? Option<V>.None() : Option<V>.Some(item.Value);
        }

        /// <summary>
        /// Streams the items of the map in-order as a series of key-value tuples
        /// </summary>
        /// <returns>Enumerable of tuples</returns>
        public IEnumerable<Tuple<K, V>> AsEnumerable()
        {
            var s = new Stack<MapItem>();
            MapItem n = root;
            while (s.Count > 0 || n != null)
            {
                // Keep going down the tree until null is found
                if (n != null)
                {
                    // Push the current value to the stack
                    s.Push(n);
                    // Go to the next smaller value
                    n = n.Left;
                }
                else
                {
                    // The last value of the stack is always the smallest
                    n = s.Pop();
                    yield return new Tuple<K,V>(n.Key,n.Value);
                    // Go to the next bigger value
                    n = n.Right;
                }
            }
        }

        /// <summary>
        /// Display the tree nodes for debugging
        /// </summary>
        public void Print()
        {
            Console.WriteLine(MapItem.DrawNode(root));
        }

        /*
        #region Recursive deletion and traversal
        // Delete recursively
        private MapItem DeleteItem(MapItem item, K key)
        {
            if (item == null) return null;
            var cmp = Comparer<K>.Default.Compare(key, item.Key);
            if (cmp < 0)
            {
                item.Right = DeleteItem(item.Left, key);
            }
            else if (cmp > 0)
            {
                item.Left = DeleteItem(item.Right, key);
            }
            else
            {
                if (item.Right == null)
                {
                    // Only left child
                    return item.Left;
                }
                else if (item.Left == null)
                {
                    // Only right child
                    return item.Right;
                }
                else
                {
                    item.Left = Swap(item.Left,item);
                }
            }
            return item;
        }

        private MapItem Swap(MapItem item, MapItem toRemove)
        {
            if (item.Right == null)
            {
                toRemove.Key = item.Key;
                toRemove.Value = item.Value;
                return item.Left;
            }
            else
            {
                item.Right = Swap(item.Right, toRemove);
                return item;
            }
        }

        // Traverse the tree recursively
        public IEnumerable<Tuple<K, V>> FetchItems()
        {
            foreach (MapItem v in FetchItems(root))
                yield return new Tuple<K, V>(v.Key,v.Value);
        }

        private IEnumerable<MapItem> FetchItems(MapItem rootNode)
        {
            if (rootNode != null)
            {
                // Recursively go down the tree to the next smaller value
                foreach (MapItem v in FetchItems(rootNode.Left)) yield return v;
                // Return the current node
                yield return rootNode;
                // Recursively go down the tree to the next larger value
                foreach (MapItem v in FetchItems(rootNode.Right)) yield return v;
            }
        }
        #endregion
        */

        /// <summary>
        /// Find a <see cref="MapItem"/> by key
        /// </summary>
        /// <param name="key">The key of the item to find</param>
        /// <returns></returns>
        private MapItem FindItem(K key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            MapItem item = root;
            int cmp;
            while (item != null)
            {
                // Compare the keys, need to use the comparer default method
                cmp = Comparer<K>.Default.Compare(key, item.Key);
                if (cmp == 0)
                {
                    // Found
                    return item;
                }

                if (cmp < 0)
                {
                    // Item is smaller than this one, go down the left subtree
                    item = item.Left;
                }
                else
                {
                    // Go down the right subtree
                    item = item.Right;
                }
            }
            // Not found, return null
            return null;
        }

        /// <summary>
        /// Insert a node to the tree recursively
        /// </summary>
        /// <param name="item">The cref="MapItem"/> node to insert at</param>
        /// <param name="newItem">The new cref="MapItem"/> to insert</param>
        private void Insert(ref MapItem item, MapItem newItem)
        {
            if (item == null)
            {
                // The tree is empty, add as new node
                item = newItem;
            }
            else
            {
                // Go down the tree
                // Check if the value is bigger
                if (Comparer<K>.Default.Compare(newItem.Key, item.Key) <= 0)
                {
                    // Go down the left subtree
                    Insert(ref item.Left, newItem);
                }
                else
                {
                    // Go down the right subtree
                    Insert(ref item.Right, newItem);
                }
            }
        }

        class MapItem
        {
            public K Key;
            public V Value;
            public MapItem Left;
            public MapItem Right;

            public MapItem(K key, V value, MapItem left = null, MapItem right = null)
            {
                Key = key;
                Value = value;
                Left = left;
                Right = right;
            }

            /// <summary>
            /// Recursively draw the tree in a flat representation
            /// </summary>
            /// <param name="item">The node to start from</param>
            /// <returns></returns>
            public static string DrawNode(MapItem item)
            {
                if (item == null) return "empty";
  
  			    if ((item.Left == null) && (item.Right == null))
  				    return item.Key.ToString();
  			    if ((item.Left != null) && (item.Right == null))
  				    return item.Key.ToString() + "(" + DrawNode (item.Left) + ", _)";

  			    if ((item.Right != null) && (item.Left == null))
  				    return item.Key.ToString() + "(_, " + DrawNode (item.Right) + ")";

                return item.Key.ToString() + "(" + DrawNode(item.Left) + ", " + DrawNode(item.Right) + ")";
            }
        }        
    }
}
