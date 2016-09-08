using DevTests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running singly-linked list tests...");
            TestList(new SingleList<string>());
            Console.WriteLine("Running doubly-linked list tests...");
            TestList(new DoubleList<string>());
            Console.WriteLine("Running map tests...");
            TestMap();
        }

        public static void TestList(ICustomList<string> list)
        {
            list.Add("A");
            list.Add("B");
            list.Add("C");
            list.Add("D");
            list.Add("E");
            list.Add("F");
            list.Add("G");
            list.Add("H");

            Debug.Assert(list.Count == 8, "List size incorrect");

            Debug.Assert(list[2] == "C", "List item with index 2 not inserted");

            Debug.Assert(list.Contains("F") == true, "List contains reports incorrect result");

            list[3] = "DA";
            Debug.Assert(list[3] == "DA", "List item with index 3 not set");;

            list.RemoveAt(2);
            Debug.Assert(list[2] != "C" && !list.Contains("C") && list.Count == 7, "List item with index 2 not deleted");

            list.Insert(2, "CD");
            Debug.Assert(list[2] == "CD" && list.Count == 8, "List item with in position 2 not inserted");

            try
            {
                list[10] = "I";
                Debug.Fail("Setting out of bounds node, expected IndexOutOfRangeException not thrown");
            }
            catch (IndexOutOfRangeException)
            {
            }

            try
            {
                list.RemoveAt(10);
                Debug.Fail("Deleting out of bounds node, expected IndexOutOfRangeException not thrown");
            }
            catch (IndexOutOfRangeException)
            {
            }

            list.RemoveAt(6);
            Debug.Assert(list[2] != "G" && !list.Contains("G") && list.Count == 7, "List item with index 6 not deleted");

            list.Insert(6, "GB");
            Debug.Assert(list[6] == "GB" && list.Count == 8, "List item not inserted");

            list.Insert(0, "Z");
            Debug.Assert(list[0] == "Z" && list[1] == "A" && list.Count == 9, "List item not inserted in head position");

            list.RemoveAt(0);
            Debug.Assert(list[0] != "Z" && !list.Contains("Z") && list.Count == 8, "Head of the list not deleted");

            bool deleted = list.Remove("E");
            Debug.Assert(deleted && !list.Contains("E") && list.Count == 7, "List item with value E not deleted");

            deleted = list.Remove("G");
            Debug.Assert(!deleted && list.Count == 7, "Not existing list value reported as deleted");

            var result = list.Where(x => x.CompareTo("D") > 0);
            var expected = new string[] { "DA", "F", "GB", "H" };
            Debug.Assert(Enumerable.SequenceEqual(expected, result), "Incorrect LINQ Where return value");

            result = list.Select(element => element.ToLower());
            expected = new string[] { "a", "b", "cd", "da", "f", "gb", "h" };
            Debug.Assert(Enumerable.SequenceEqual(expected, result), "Incorrect LINQ Select return value");

            var result2 = list.SelectMany(element => element.ToCharArray());
            var expected2 = new char[] { 'A', 'B', 'C', 'D', 'D', 'A', 'F', 'G', 'B', 'H' };
            Debug.Assert(Enumerable.SequenceEqual(expected2, result2), "Incorrect LINQ SelectMany return value");

            list.Clear();
            Debug.Assert(list.Count == 0, "List not cleared");

            list.Add("A");
            Debug.Assert(list[0] == "A" && list.Count == 1, "Insertion incorrect after clearing list");
            list.RemoveAt(0);
            Debug.Assert(!list.Contains("A") && list.Count == 0, "Deletion incorrect after clearing list");
        }

        private static void TestMap()
        {
            var map = new Map<int, string>();

            map.Add(42, "A");
            map.Add(25, "B");
            map.Add(65, "C");
            map.Add(12, "D");
            map.Add(37, "E");
            map.Add(13, "F");
            map.Add(30, "G");
            map.Add(43, "H");
            map.Add(87, "I");
            map.Add(99, "J");
            map.Add(9, "K");

            Debug.Assert(map.Count == 11, "Map size incorrect");

            Debug.Assert(map[65] == "C", "Map item (65,C) not inserted");

            Debug.Assert(map.ContainsKey(13) == true, "Map ContainsKey reports incorrect result");

            Debug.Assert(map.ContainsValue("H") == true, "Map ContainsValue reports incorrect result");

            var option = map.TryGetValue(37);
            Debug.Assert(option.HasValue == true && option.Value == "E", "Map TryGetValue returns incorrect result");

            try
            {
                map[10] = "Z";
                Debug.Fail("Setting out of bounds node, expected IndexOutOfRangeException not thrown");
            }
            catch (KeyNotFoundException)
            {
            }

            var deleted = map.Remove(10);
            Debug.Assert(!deleted && map.Count == 11, "Not existing map value reported as deleted");

            map[12] = "DA";
            Debug.Assert(map[12] == "DA", "Map item (12,DA) not set");

            map.Add(23, "L");
            Debug.Assert(map[23] == "L" && map.ContainsKey(23) && map.Count == 12, "Map item (23,L) not inserted");

            deleted = map.Remove(87);
            Debug.Assert(!map.ContainsKey(87) && map.Count == 11, "Node with single right child not deleted");

            deleted = map.Remove(25);
            Debug.Assert(!map.ContainsKey(25) && map.Count == 10, "Node with both children not deleted");

            deleted = map.Remove(99);
            Debug.Assert(!map.ContainsKey(99) && map.Count == 9, "Node with no children not deleted");

            deleted = map.Remove(65);
            Debug.Assert(!map.ContainsKey(65) && map.Count == 8, "Node with single left child not deleted");

            deleted = map.Remove(42);
            Debug.Assert(!map.ContainsKey(42) && map.Count == 7, "Root node not deleted");

            var result = map.Where(x => x.Item1 > 30);
            var expected = new Tuple<int,string>[]
            {
                Tuple.Create(37, "E"),
                Tuple.Create(43, "H")
            };
            Debug.Assert(Enumerable.SequenceEqual(expected, result), "Incorrect LINQ Where return value (expression using key)");

            result = map.Where(x => x.Item2.CompareTo("H") > 0);
            expected = new Tuple<int, string>[]
            {
                Tuple.Create(9, "K"), Tuple.Create(23, "L")
            };
            Debug.Assert(Enumerable.SequenceEqual(expected, result), "Incorrect LINQ Where return value (expression using value)");

            var result2 = map.Select(element => Tuple.Create(element.Item1 * 2, element.Item2 + element.Item2.ToLower()));
            expected = new Tuple<int, string>[]
            {
                Tuple.Create(18,"Kk"), Tuple.Create(24, "DAda"), Tuple.Create(26, "Ff"),
                Tuple.Create(46, "Ll"), Tuple.Create(60, "Gg"), Tuple.Create(74, "Ee"),
                Tuple.Create(86, "Hh")
            };
            Debug.Assert(Enumerable.SequenceEqual(expected, result2), "Incorrect LINQ Select return value");

            var expected3 = new int[] { 43, 30, 37, 12, 13, 23, 9 };
            Debug.Assert(Enumerable.SequenceEqual(expected3, map.Keys), "Incorrect Keys return value");

            var expected4 = new string[] { "H", "G", "E", "DA", "F", "L", "K" };
            Debug.Assert(Enumerable.SequenceEqual(expected4, map.Values), "Incorrect Values return value");

            var expected5 = new List<Tuple<int, string>>
            {
                Tuple.Create(9,"K"), Tuple.Create(12, "DA"), Tuple.Create(13, "F"),
                Tuple.Create(23, "L"), Tuple.Create(30, "G"), Tuple.Create(37, "E"),
                Tuple.Create(43, "H")
            };
            Debug.Assert(Enumerable.SequenceEqual(expected5, map.AsEnumerable()), "Incorrect AsEnumerable return value");

            map.Clear();
            Debug.Assert(map.Count == 0, "List not cleared");

            map.Add(34,"A");
            Debug.Assert(map[34] == "A" && map.Count == 1, "Insertion incorrect after clearing map");
            map.Remove(34);
            Debug.Assert(!map.ContainsKey(34) && map.Count == 0, "Deletion incorrect after clearing map");
        }
    }
}
