using System;
using System.Net;
using System.Text;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.Nord24Ru
{
    public class Nord24RuWebSite : WebSite
    {
        private readonly Nord24RuCity _city;

        public Nord24RuWebSite(Nord24RuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return Nord24RuCompany.Instance; }
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
                    ProductActicle = Nord24RuConstants.SupportArticles,
                    PriceTypes = Nord24RuConstants.SupportedPriceTypes,
                    ProxyTimeout = Nord24RuConstants.ProxyTimeout,
                    AvailabilityInShops = Nord24RuConstants.SupportShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = Nord24RuConstants.ExcludeKeywords,
                        ExcludePaths = Nord24RuConstants.ExcludePaths
                    }
                };
            }
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var cookie = new Cookie("city", _city.CookieValue, "/", "www.nord24.ru");
            request.Cookies.Add(cookie);
            return base.LoadPageContent(request, context);
        }
    }
}