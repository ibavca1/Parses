using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using EnterpriseLibrary.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit
{
    public static class WebParsingAsserts
    {
        public static void AreEqual(this IAssertion assertion, Uri expected, Uri actual)
        {
            var expectedBuilder = new UriBuilder(expected);
            NameValueCollection expectedQuery = HttpUtility.ParseQueryString(expectedBuilder.Query);

            var actualBuilder = new UriBuilder(actual);
            NameValueCollection actualQuery = HttpUtility.ParseQueryString(actualBuilder.Query);

            bool areEqual =
                string.Equals(expectedBuilder.Scheme, actualBuilder.Scheme, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(expectedBuilder.Host, actualBuilder.Host, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(expectedBuilder.Path, actualBuilder.Path, StringComparison.InvariantCultureIgnoreCase) &&
                AreEqual(expectedQuery, actualQuery);

            if (!areEqual)
                assertion.Fail("Assert failed. Expected:<{0}>. Actual:<{1}>", expected, actual);
        }

        public static void AreSame(this IAssertion assertion, Uri expected, Uri actual)
        {
            var expectedBuilder = new UriBuilder(expected);
            NameValueCollection expectedQuery = HttpUtility.ParseQueryString(expectedBuilder.Query);

            var actualBuilder = new UriBuilder(actual);
            NameValueCollection actualQuery = HttpUtility.ParseQueryString(actualBuilder.Query);

            bool areEquvalent =
                string.Equals(expectedBuilder.Scheme, actualBuilder.Scheme, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(expectedBuilder.Host, actualBuilder.Host, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(expectedBuilder.Path, actualBuilder.Path, StringComparison.InvariantCultureIgnoreCase) &&
                AreSame(expectedQuery, actualQuery);

            if (!areEquvalent)
                assertion.Fail("Assert failed. Expected:<{0}>. Actual:<{1}>", expected, actual);
        }

        public static void AreEqual(this IAssertion assertion, NameValueCollection expected, NameValueCollection actual)
        {
            if (!AreEqual(expected, actual))
                assertion.Fail("Assert failed. Expected:<{0}>. Actual:<{1}>", expected, actual);
        }

        private static bool AreEqual(NameValueCollection expected, NameValueCollection actual)
        {
            return
                expected
                    .AllKeys.OrderBy(key => key)
                    .SequenceEqual(actual.AllKeys.OrderBy(key => key)) &&
                AreSame(expected, actual);

        }

        private static bool AreSame(NameValueCollection expected, NameValueCollection actual)
        {
            return
                expected.AllKeys.All(
                    key => string.Equals(expected[key], actual[key], StringComparison.InvariantCultureIgnoreCase));
        }
    }

    public static class WebPageAsserts
    {
        public static void AreEqual(this IAssertion assertion, WebPage expected, WebPage actual)
        {
            assertion.AreSame(expected.Uri, actual.Uri);
            assertion.AreEqual(expected.Type, actual.Type);
            assertion.AreEqual(expected.Path, actual.Path);
        }
    }
}