using System;
using System.Net;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.UlmartRu
{
    public class UlmartRuWebSite : WebSite
    {
        private readonly UlmartRuCity _city;

        public UlmartRuWebSite(UlmartRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return UlmartRuCompany.Instance; }
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
                    ProductActicle = UlmartRuConstants.SupportArticles,
                    PriceTypes = UlmartRuConstants.SupportedPriceTypes,
                    ProxyTimeout = UlmartRuConstants.ProxyTimeout,
                    AvailabilityInShops = UlmartRuConstants.SupportAvailabilityInShops,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = UlmartRuConstants.ExcludeKeywords,
                        ExcludePaths = UlmartRuConstants.ExcludePaths
                    }
                };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if ((UlmartRuWebPageType)type == UlmartRuWebPageType.Catalog)
            {
                uri = uri
                    .AddQueryParam("sort", "2")
                    .AddQueryParam("viewType", "1")
                    .AddQueryParam("rec", "true");
            }

            return base.GetPage(uri, type, path);
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var cookie = new Cookie("city", _city.CookieValue, "/", "ulmart.ru");
            request.Cookies.Add(cookie);
            return base.LoadPageContent(request, context);
        }
    }
}