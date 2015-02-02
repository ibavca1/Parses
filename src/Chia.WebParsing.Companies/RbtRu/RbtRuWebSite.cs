using System;
using System.Net;
using System.Text;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.RbtRu
{
    public class RbtRuWebSite : WebSite
    {
        private readonly RbtRuCity _city;

        public RbtRuWebSite(RbtRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return RbtRuCompany.Instance; }
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
                    ProductActicle = RbtRuConstants.SupportArticles,
                    PriceTypes = RbtRuConstants.SupportedPriceTypes,
                    ProxyTimeout = RbtRuConstants.ProxyTimeout,
                    AvailabilityInShops = RbtRuConstants.SupportShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = RbtRuConstants.ExcludeKeywords,
                        ExcludePaths = RbtRuConstants.ExcludePaths
                    }
                };
            }
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var regionCookie = new Cookie("region", _city.CookieValue, "/", ".rbt.ru");
            request.Cookies.Add(regionCookie);

            if (RbtRuWebPageType.Catalog == (RbtRuWebPageType)request.Type)
            {
                var pageSizeCookie = new Cookie("ItemsOnPage", "45", "/", "www.rbt.ru");
                request.Cookies.Add(pageSizeCookie);
            }

            return base.LoadPageContent(request, context);
        }
    }
}