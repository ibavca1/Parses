using System;
using System.Net;
using System.Text;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.RegardRu
{
    public class RegardRuWebSite : WebSite
    {
        private readonly RegardRuCity _city;

        public RegardRuWebSite(RegardRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return RegardRuCompany.Instance; }
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
                    ProductActicle = RegardRuConstants.SupportArticles,
                    PriceTypes = RegardRuConstants.SupportedPriceTypes,
                    ProxyTimeout = RegardRuConstants.ProxyTimeout,
                    AvailabilityInShops = RegardRuConstants.SupportShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = RegardRuConstants.ExcludeKeywords,
                        ExcludePaths = RegardRuConstants.ExcludePaths
                    }
                };
            }
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var cookies = new CookieCollection
                              {
                                  new Cookie("view", "1", "/", "www.regard.ru"),
                                  new Cookie("sorting", "name_asc", "/", "www.regard.ru"),
                                  new Cookie("page_limit", "100", "/", "www.regard.ru")
                              };
            request.Cookies.Add(cookies);

            return base.LoadPageContent(request, context);
        }
    }
}