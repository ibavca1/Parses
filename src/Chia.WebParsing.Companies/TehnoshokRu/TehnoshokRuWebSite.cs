using System;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.TehnoshokRu
{
    public class TehnoshokRuWebSite : WebSite
    {
        public const string UriMask = TehnoshokRuConstants.UriMask;
        private readonly TehnoshokRuCity _city;

        public TehnoshokRuWebSite(TehnoshokRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return TehnoshokRuCompany.Instance; }
        }

        public override WebCity City
        {
            get { return _city; }
        }

        public override Uri Uri
        {
            get
            {
                if (TehnoshokRuCity.StPetersburg.Equals(_city))
                    return Company.SiteUri;

                string address = string.Format(UriMask, _city.UriPrefix);
                return new Uri(address);
            }
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
                    ProductActicle = TehnoshokRuConstants.SupportArticles,
                    PriceTypes = TehnoshokRuConstants.SupportedPriceTypes,
                    ProxyTimeout = TehnoshokRuConstants.ProxyTimeout,
                    AvailabilityInShops = TehnoshokRuConstants.SupportShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = TehnoshokRuConstants.ExcludeKeywords,
                        ExcludePaths = TehnoshokRuConstants.ExcludePaths
                    }
                };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if ((TehnoshokRuWebPageType)type == TehnoshokRuWebPageType.Catalog)
            {
                uri = uri
                    .AddQueryParam("sort", "price")
                    .AddQueryParam("order", "asc");
            }

            return base.GetPage(uri, type, path);
        }
    }
}