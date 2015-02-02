using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

namespace WebParsingFramework
{
    /// <summary>
    /// NameValueCollection to represent form data and to generate form data output.
    /// </summary>
    [Serializable]
    public class HttpValueCollection : NameValueCollection
    {
        public HttpValueCollection()
            : base(StringComparer.OrdinalIgnoreCase) // case-insensitive keys
        {
        }

        public HttpValueCollection(NameValueCollection collection)
            : this()
        {
            Add(collection);
        }

        // Use a builder function instead of a ctor to avoid virtual calls from the ctor.
        public static NameValueCollection Create()
        {
            return new HttpValueCollection();
        }

        public static NameValueCollection Create(IEnumerable<KeyValuePair<string, string>> pairs)
        {
            var nvc = new HttpValueCollection();

            // Ordering example:
            // k=A&j=B&k=C --> k:[A,C];j=[B].
            foreach (KeyValuePair<string, string> kv in pairs)
            {
                string key = kv.Key ?? String.Empty;
                string value = kv.Value ?? String.Empty;
                nvc.Add(key, value);
            }

            nvc.IsReadOnly = false;
            return nvc;
        }


        protected HttpValueCollection(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override string ToString()
        {
            return ToString(true);
        }

        private string ToString(bool urlEncode)
        {
            if (Count == 0)
            {
                return String.Empty;
            }

            var builder = new StringBuilder();
            bool first = true;
            foreach (string name in this)
            {
                string[] values = GetValues(name);
                if (values == null || values.Length == 0)
                {
                    first = AppendNameValuePair(builder, first, urlEncode, name, String.Empty);
                }
                else
                {
                    foreach (string value in values)
                    {
                        first = AppendNameValuePair(builder, first, urlEncode, name, value);
                    }
                }
            }

            return builder.ToString();
        }

        private static bool AppendNameValuePair(StringBuilder builder, bool first, bool urlEncode, string name, string value)
        {
            string effectiveName = name ?? String.Empty;
            string encodedName = urlEncode ? HttpUtility.UrlEncode(effectiveName) : effectiveName;

            string effectiveValue = value ?? String.Empty;
            string encodedValue = urlEncode ? HttpUtility.UrlEncode(effectiveValue) : effectiveValue;

            if (first)
            {
                first = false;
            }
            else
            {
                builder.Append("&");
            }

            builder.Append(encodedName);
            if (!String.IsNullOrEmpty(encodedValue))
            {
                builder.Append("=");
                builder.Append(encodedValue);
            }
            return first;
        }
    }
}