using System.Collections.Specialized;

namespace WebParsingFramework.Utils
{
    public static class NameValueCollectionExtensions
    {
        public static void AddOrReplace(this NameValueCollection dest, NameValueCollection source)
        {
            foreach (string key in source.Keys)
            {
                dest[key] = source[key];
            }
        }
    }
}