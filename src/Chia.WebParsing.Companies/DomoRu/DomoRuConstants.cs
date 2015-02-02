using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.DomoRu
{
    internal static class DomoRuConstants
    {
        public const int Id = 83;

        public const string Name = "Domo.Ru";

        public const string SiteUri = "http://www.domo.ru/";

        public const string SiteUriMask = "http://{0}.domo.ru/";

        public const bool AvailabilityInShops = false;

        public const bool ProductArticle = true;

        public const WebPriceType PriceTypes = WebPriceType.Internet;

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);
        
        public static readonly WebCity[] Cities = DomoRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
           new WebSiteMapPath[]
                {

                };

        public static readonly string[] ExcludeKeywords =
            new string[]
                {

                };
    }
}