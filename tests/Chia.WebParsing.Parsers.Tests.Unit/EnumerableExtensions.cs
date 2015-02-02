using System;
using System.Collections.Generic;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit
{
    internal static class EnumerableExtensions
    {
        public static IEnumerable<WebPage> WhereType<TPageType>(this IEnumerable<WebPage> collection, TPageType type)
        {
            return collection.Where(p => Convert.ToInt32(type) == Convert.ToInt32(p.Type));
        }

        public static WebPage FirstWithType<TPageType>(this IEnumerable<WebPage> collection, TPageType type)
            where TPageType : struct
        {
            return collection.First(p => Convert.ToInt32(type) == Convert.ToInt32(p.Type));
        }

        public static WebPage FirstOrDefaultWithType<TPageType>(this IEnumerable<WebPage> collection, TPageType type)
            where TPageType : struct
        {
            return collection.FirstOrDefault(p => Convert.ToInt32(type) == Convert.ToInt32(p.Type));
        }

        public static WebPage SingleWithType<TPageType>(this IEnumerable<WebPage> collection, TPageType type)
           where TPageType : struct
        {
            return collection.Single(p => Convert.ToInt32(type) == Convert.ToInt32(p.Type));
        }

        public static WebPage SingleOrDefaultWithType<TPageType>(this IEnumerable<WebPage> collection, TPageType type)
            where TPageType : struct
        {
            return collection.SingleOrDefault(p => Convert.ToInt32(type) == Convert.ToInt32(p.Type));
        }
    }
}