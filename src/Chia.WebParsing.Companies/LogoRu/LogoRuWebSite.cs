using System;
using System.Net;
using System.Text;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.LogoRu
{
    public class LogoRuWebSite : WebSite
    {
        private readonly LogoRuCity _city;

        public LogoRuWebSite(LogoRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return LogoRuCompany.Instance; }
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
                    ProductActicle = LogoRuConstants.SupportArticles,
                    PriceTypes = LogoRuConstants.SupportedPriceTypes,
                    ProxyTimeout = LogoRuConstants.ProxyTimeout,
                    AvailabilityInShops = LogoRuConstants.SupportShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = LogoRuConstants.ExcludeKeywords,
                        ExcludePaths = LogoRuConstants.ExcludePaths
                    }
                };
            }
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var cookie = new Cookie("city", _city.CookieValue, "/", "www.logo.ru");
            request.Cookies.Add(cookie);
            return base.LoadPageContent(request, context);
        }
    }
}