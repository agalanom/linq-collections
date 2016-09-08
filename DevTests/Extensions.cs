using System;
using System.Collections.Generic;

namespace DevTests
{
    public static class Extensions
    {
        #region SingleList and DoubleList
        public static IEnumerable<TSource> Where<TSource>(this ICustomList<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TSource> Where<TSource>(this ICustomList<TSource> source, Func<TSource, int, bool> predicate)
        {
            int index = 0;
            foreach (TSource item in source)
            {
                if (predicate(item, index))
                {
                    yield return item;
                }
                index++;
            }
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this ICustomList<TSource> source, Func<TSource, TResult> selector)
        {
            foreach (TSource item in source)
            {
                yield return selector(item);
            }
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this ICustomList<TSource> source, Func<TSource, int, TResult> selector)
        {
            int index = 0;
            foreach (TSource item in source)
            {
                yield return selector(item, index);
            }
        }

        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this ICustomList<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            foreach (TSource item in source)
            {
                foreach (TResult result in selector(item))
                {
                    yield return result;
                }
            }
        }

        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this ICustomList<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
        {
            int index = 0;
            foreach (TSource item in source)
            {
                foreach (TResult result in selector(item, index++))
                {
                    yield return result;
                }
            }
        }

        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this ICustomList<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {

            foreach (TSource item in source)
            {
                foreach (TCollection collectionItem in collectionSelector(item))
                {
                    yield return resultSelector(item, collectionItem);
                }
            }
        }

        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this ICustomList<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            int index = 0;
            foreach (TSource item in source)
            {
                foreach (TCollection collectionItem in collectionSelector(item, index++))
                {
                    yield return resultSelector(item, collectionItem);
                }
            }
        }
        #endregion

        #region Map
        public static IEnumerable<Tuple<K, V>> Where<K, V>(this Map<K, V> source, Func<Tuple<K, V>, bool> predicate)
        {
            foreach (Tuple<K, V> item in source.AsEnumerable())
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<Tuple<K, V>> Where<K, V>(this Map<K, V> source, Func<Tuple<K, V>, int, bool> predicate)
        {
            int index = 0;
            foreach (Tuple<K, V> item in source.AsEnumerable())
            {
                if (predicate(item, index))
                {
                    yield return item;
                }
                index++;
            }
        }

        public static IEnumerable<TResult> Select<K, V, TResult>(this Map<K, V> source, Func<Tuple<K, V>, TResult> selector)
        {
            foreach (Tuple<K, V> item in source.AsEnumerable())
            {
                yield return selector(item);
            }
        }

        public static IEnumerable<TResult> Select<K, V, TResult>(this Map<K, V> source, Func<Tuple<K, V>, int, TResult> selector)
        {
            int index = 0;
            foreach (Tuple<K, V> item in source.AsEnumerable())
            {
                yield return selector(item, index);
            }
        }
        #endregion
    }
}
