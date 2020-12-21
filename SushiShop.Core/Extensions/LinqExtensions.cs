using SushiShop.Core.Common.Comparers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SushiShop.Core.Extensions
{
    public static class LinqExtensions
    {
        public static object? ElementAtOrDefault(this IEnumerable source, int index)
        {
            if (source is null)
            {
                return null;
            }

            if (index < 0)
            {
                return null;
            }

            if (source is IList list)
            {
                return index < list.Count ? list[index] : null;
            }

            var currentIndex = 0;
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (currentIndex == index)
                {
                    return enumerator.Current;
                }

                ++currentIndex;
            }

            return null;
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        public static IEnumerable<T> Pipe<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
                yield return item;
            }
        }

        public static IEnumerable<T> ExceptBy<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> comparerFunc)
        {
            return first.Except(second, new DelegateEqualityComparer<T>(comparerFunc, null));
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.DistinctBy(keySelector, null);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
                                                                     Func<TSource, TKey> keySelector,
                                                                     IEqualityComparer<TKey>? comparer)
        {
            var knownKeys = new HashSet<TKey>(comparer);
            foreach (var element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
