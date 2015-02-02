using System;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.DostavkaRu
{
    public class DostavkaRuWebSite : WebSite
    {
        private readonly WebCity _city;

        public DostavkaRuWebSite(WebCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return DostavkaRuCompany.Instance; }
        }

        public override WebCity City
        {
            get { return _city; }
        }

        public override Uri Uri
        {
            get { return Company.SiteUri; }
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        public override WebSiteMetadata Metadata
        {
            get
            {
                return new WebSiteMetadata
                {
                    ProductActicle = DostavkaRuConstants.SupportArticles,
                    PriceTypes = DostavkaRuConstants.SupportedPriceTypes,
                    ProxyTimeout = DostavkaRuConstants.ProxyTimeout,
                    AvailabilityInShops = DostavkaRuConstants.SupportShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = DostavkaRuConstants.ExcludeKeywords,
                        ExcludePaths = DostavkaRuConstants.ExcludePaths
                    }
                };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if ((DostavkaRuWebPageType)type == DostavkaRuWebPageType.Catalog)
            {
                uri = uri
                    .AddQueryParam("limit", "2")
                    .AddQueryParam("view", "list");
            }

            return base.GetPage(uri, type, path);
        }
    }
}