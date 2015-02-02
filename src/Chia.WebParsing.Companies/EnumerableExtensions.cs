using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Chia.WebParsing.Companies
{
    public static class EnumerableExtensions
    {
        [Pure]
        public static bool IsUnique<T>(this IEnumerable<T> collection)
        {
// ReSharper disable PossibleMultipleEnumeration
            return collection.Count() == collection.Distinct().Count();
// ReSharper enable PossibleMultipleEnumeration
        }
    }
}