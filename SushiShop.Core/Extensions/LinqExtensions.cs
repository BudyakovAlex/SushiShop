using System.Collections;

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
    }
}
