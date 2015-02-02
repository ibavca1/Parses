using System;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.OnlinetradeRu
{
    public class OnlinetradeRuWebSite : WebSite
    {
        private readonly OnlinetradeRuCity _city;
        private const string UriMask = OnlinetradeRuConstants.UriMask;

        public OnlinetradeRuWebSite(OnlinetradeRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return OnlinetradeRuCompany.Instance; }
        }

        public override WebCity City
        {
            get { return _city; }
        }

        public override Uri Uri
        {
            get
            {
                string address = string.Format(UriMask, _city.UriPrefix);
                return new Uri(address);
            }
        }

        public override Encoding Encoding
        {
            get { return Encoding.GetEncoding(1251); }
        }

        public override WebSiteMetadata Metadata
        {
            get
            {
                return new WebSiteMetadata
                {
                    ProductActicle = OnlinetradeRuConstants.SupportArticles,
                    PriceTypes = OnlinetradeRuConstants.SupportedPriceTypes,
                    ProxyTimeout = OnlinetradeRuConstants.ProxyTimeout,
                    AvailabilityInShops = OnlinetradeRuConstants.SupportShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = OnlinetradeRuConstants.ExcludeKeywords,
                        ExcludePaths = OnlinetradeRuConstants.ExcludePaths
                    }
                };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if ((OnlinetradeRuWebPageType)type == OnlinetradeRuWebPageType.Catalog)
            {
                uri = uri
                    .AddQueryParam("per_page", "50")
                    .AddQueryParam("browse_mode", "2")
                    .AddQueryParam("sort", "title-asc");
            }

            return base.GetPage(uri, type, path);
        }
    }
}