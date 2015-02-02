using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.TenIRu
{
    internal static class TenIRuConstants
    {
        public const int Id = 603;

        public const string Name = "10i.Ru";

        public const string SiteUri = "http://www.10i.ru";

        public const bool Articles = true;

        public static readonly WebCity[] Cities = TenIRuCity.All.ToArray();

        public const WebPriceType PriceTypes = WebPriceType.Internet;

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool ShopsAvailability = true;

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