using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chia.WebParsing.Parsers.Tests.Unit
{
    public static class AssertEx
    {
        public static void IsTrueForAll<T>(IEnumerable<T> collection, Func<T,bool> predicate)
        {
            if (!collection.All(predicate))
                throw new AssertFailedException("AssertEx.IsTrueForAll");
        }

        public static void IsFalseForAll<T>(IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (collection.Any(predicate))
                throw new AssertFailedException("AssertEx.IsTrueForAll");
        }

        public static void AreEqual(Uri expected, Uri actual)
        {
            var expectedBuilder = new UriBuilder(expected);
            NameValueCollection expectedQuery = HttpUtility.ParseQueryString(expectedBuilder.Query);

            var actualBuilder = new UriBuilder(actual);
            NameValueCollection actualQuery = HttpUtility.ParseQueryString(actualBuilder.Query);
           
            Assert.AreEqual(expectedBuilder.Scheme, actualBuilder.Scheme);
            Assert.AreEqual(expectedBuilder.Host, actualBuilder.Host);
            Assert.AreEqual(expectedBuilder.Path, actualBuilder.Path);
            AssertEx.AreEqual(expectedQuery, actualQuery);
        }

        public static void AreEqual(NameValueCollection expected, NameValueCollection actual)
        {
            bool areEqual =
                expected.AllKeys.OrderBy(key => key)
                    .SequenceEqual(actual.AllKeys.OrderBy(key => key))
            && expected.AllKeys.All(key => expected[key] == actual[key]);

            if (!areEqual)
            {
                string message =
                    string.Format("AssertEx failed. Expected:<{0}>. Actual:<{1}>", expected, actual);
                throw new AssertFailedException(message);
            }
        }
    }

    public static class CollectionAssertEx
    {
        public static void IsTrueForAll<T>(IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (!collection.All(predicate))
                throw new AssertFailedException("AssertEx.IsTrueForAll");
        }

        public static void IsFalseForAll<T>(IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (collection.Any(predicate))
                throw new AssertFailedException("AssertEx.IsTrueForAll");
        }
    }
}