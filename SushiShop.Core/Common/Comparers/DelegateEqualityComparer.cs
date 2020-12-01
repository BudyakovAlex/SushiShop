using System;
using System.Collections.Generic;

namespace SushiShop.Core.Common.Comparers
{
    public class DelegateEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _comparerFunc;
        private readonly Func<T, int>? _getHashCodeFunc;

        public DelegateEqualityComparer(Func<T, T, bool> comparerFunc, Func<T, int>? getHashCodeFunc = null)
        {
            _comparerFunc = comparerFunc;
            _getHashCodeFunc = getHashCodeFunc;
        }

        public bool Equals(T first, T second)
        {
            return ReferenceEquals(first, second) || (first != null && second != null && _comparerFunc.Invoke(first, second));
        }

        public int GetHashCode(T obj)
        {
            return _getHashCodeFunc?.Invoke(obj) ?? -1;
        }
    }
}