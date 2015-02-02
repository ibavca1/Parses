using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.TechnonetRu
{
    internal static class TechnonetRuConstants
    {
        public const int Id = 272;

        public const string Name = "Technonet.Ru";

        public const string SiteUri = "http://Technonet.ru/";

        public const bool Articles = false;

        public static readonly WebCity[] Cities = TechnonetRuCity.All.ToArray();

        public const WebPriceType PriceTypes = WebPriceType.Internet | WebPriceType.Retail;

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