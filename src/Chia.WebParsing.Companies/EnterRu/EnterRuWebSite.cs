using System;
using System.Net;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.EnterRu
{
    public class EnterRuWebSite : WebSite
    {
        private readonly EnterRuCity _city;

        public EnterRuWebSite(EnterRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return EnterRuCompany.Instance; }
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
                    ProductActicle = EnterRuConstants.SupportArticles,
                    PriceTypes = EnterRuConstants.SupportedPriceTypes,
                    ProxyTimeout = EnterRuConstants.ProxyTimeout,
                    AvailabilityInShops = EnterRuConstants.SupportShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = EnterRuConstants.ExcludeKeywords,
                        ExcludePaths = EnterRuConstants.ExcludePaths
                    }
                };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if (EnterRuWebPageType.Catalog == (EnterRuWebPageType)type)
            {
                uri = uri
                   .AddQueryParam("sort", "price-asc");
            }

            return base.GetPage(uri, type, path);
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var cookie = new Cookie("geoshop", _city.CookieValue, "/", ".enter.ru");
            request.Cookies.Add(cookie);
            return base.LoadPageContent(request, context);
        }
    }
}