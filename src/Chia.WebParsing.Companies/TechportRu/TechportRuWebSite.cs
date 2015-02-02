using System;
using System.Net;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.TechportRu
{
    public class TechportRuWebSite : WebSite
    {
        private readonly TechportRuCity _city;

        public TechportRuWebSite(TechportRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return TechportRuCompany.Instance; }
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
                    ProductActicle = TechportRuConstants.SupportArticles,
                    PriceTypes = TechportRuConstants.SupportedPriceTypes,
                    ProxyTimeout = TechportRuConstants.ProxyTimeout,
                    AvailabilityInShops = TechportRuConstants.SupportShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = TechportRuConstants.ExcludeKeywords,
                        ExcludePaths = TechportRuConstants.ExcludePaths
                    }
                };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if ((TechportRuWebPageType)type == TechportRuWebPageType.Catalog)
            {
                uri = uri
                    .AddQueryParam("sort", "abc") // сортировка по названию
                    .AddQueryParam("sdim", "asc");
            }

            return base.GetPage(uri, type, path);
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var cookie = new Cookie("usc", _city.CookieValue, "/", ".techport.ru");
            request.Cookies.Add(cookie);
            return base.LoadPageContent(request, context);
        }
    }
}