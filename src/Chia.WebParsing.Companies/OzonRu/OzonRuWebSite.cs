using System;
using System.Text;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.OzonRu
{
    public class OzonRuWebSite : WebSite
    {
        private readonly WebCity _city;

        public OzonRuWebSite(WebCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return OzonRuCompany.Instance; }
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
            get { return Encoding.GetEncoding(1251); }
        }

        public override WebSiteMetadata Metadata
        {
            get
            {
                return new WebSiteMetadata
                {
                    ProductActicle = OzonRuConstants.SupportArticles,
                    PriceTypes = OzonRuConstants.SupportedPriceTypes,
                    ProxyTimeout = OzonRuConstants.ProxyTimeout,
                    AvailabilityInShops = OzonRuConstants.SupportShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = OzonRuConstants.ExcludeKeywords,
                        ExcludePaths = OzonRuConstants.ExcludePaths
                    }
                };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            bool isListPage =
                uri.AbsolutePath.IndexOf("catalog", StringComparison.InvariantCultureIgnoreCase) != -1;
            if (isListPage)
                type = (WebPageType)OzonRuWebPageType.Catalog;

            return base.GetPage(uri, type, path);
        }
    }
}