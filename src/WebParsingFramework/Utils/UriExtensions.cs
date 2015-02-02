using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace WebParsingFramework.Utils
{
    /// <summary>
    /// Методы-расширения для класса <see cref="Uri"/>.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Adds the specified parameter to the Query String.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramName">Name of the parameter to add.</param>
        /// <param name="paramValue">Value for the parameter to add.</param>
        /// <returns>Url with added parameter.</returns>
        public static Uri AddQueryParam(this Uri url, string paramName, string paramValue)
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue;
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        public static Uri AddOrReplaceQueryParam(this Uri url, string paramName, string paramValue)
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue;
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }


        public static Uri RemoveQueryParam(this Uri url, string paramName)
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Remove(paramName);
            uriBuilder.Query = query.ToString();
            return uriBuilder.Uri;
        }


        public static Uri AddQueryParams(this Uri uri, NameValueCollection parms)
        {
            NameValueCollection uriQuery = HttpUtility.ParseQueryString(uri.Query);
            uriQuery.Add(parms);
            uri = new UriBuilder(uri) { Query = uriQuery.ToString() }.Uri;
            return uri;
        }

        public static Uri AddOrReplaceQueryParams(this Uri uri, NameValueCollection parms)
        {
            NameValueCollection uriQuery = HttpUtility.ParseQueryString(uri.Query);
            uriQuery.AddOrReplace(parms);
            uri = new UriBuilder(uri) { Query = uriQuery.ToString() }.Uri;
            return uri;
        }

        public static NameValueCollection GetQueryParams(this Uri uri)
        {
            return HttpUtility.ParseQueryString(uri.Query);
        }

        public static string GetQueryParam(this Uri uri, string paramName)
        {
            return uri.GetQueryParams()[paramName];
        }

        public static Uri RemoveQuery(this Uri uri)
        {
            var builder = new UriBuilder(uri) { Query = string.Empty };
            return builder.Uri;
        }

        public static Uri HttpEncode(this Uri uri)
        {
            string initalUri = uri.ToString();
            string encoded = HttpUtility.HtmlEncode(initalUri);
            return new Uri(encoded);
        }

        public static Uri Encode(this Uri uri)
        {
            string path = uri.AbsolutePath;
            path = HttpUtility.UrlPathEncode(path);
            var builder = new UriBuilder(uri) { Path = path };
            return builder.Uri;
        }

        public static bool IsAbsoluteUrl(this string url)
        {
            Uri uri;
            return Uri.TryCreate(url, UriKind.Absolute, out uri);
        }

        public static bool IsSame(this Uri uri, Uri other)
        {
            var expectedBuilder = new UriBuilder(uri);
            NameValueCollection expectedQuery = uri.GetQueryParams();

            var actualBuilder = new UriBuilder(other);
            NameValueCollection actualQuery = HttpUtility.ParseQueryString(actualBuilder.Query);

            return 
                string.Equals(expectedBuilder.Scheme, actualBuilder.Scheme, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(expectedBuilder.Host, actualBuilder.Host, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(expectedBuilder.Path, actualBuilder.Path, StringComparison.InvariantCultureIgnoreCase) &&
                AreSame(expectedQuery, actualQuery);
        }

        private static bool AreSame(NameValueCollection expected, NameValueCollection actual)
        {
            foreach (string key in expected.AllKeys)
            {
                string expectedValue = expected[key];
                string actualValue = actual[key];

                if (!string.Equals(expectedValue, actualValue, StringComparison.InvariantCulture))
                    return false;
            }

            return true;
        }

        public static Uri Append(this Uri uri, params string[] paths)
        {
            return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => string.Format("{0}/{1}", current.TrimEnd('/'), path.TrimStart('/'))));
        }
    }
}