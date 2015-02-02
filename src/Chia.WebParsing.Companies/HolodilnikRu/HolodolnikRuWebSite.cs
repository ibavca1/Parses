using System;
using System.Net;
using System.Text;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.HolodilnikRu
{
    public class HolodilnikRuWebSite : WebSite
    {
        private readonly HolodilnikRuCity _city;

        public HolodilnikRuWebSite(HolodilnikRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return HolodilnikRuCompany.Instance; }
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
                    ProductActicle = HolodilnikRuConstants.SupportArticles,
                    PriceTypes = HolodilnikRuConstants.SupportedPriceTypes,
                    ProxyTimeout = HolodilnikRuConstants.ProxyTimeout,
                    AvailabilityInShops = HolodilnikRuConstants.SupportShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = HolodilnikRuConstants.ExcludeKeywords,
                        ExcludePaths = HolodilnikRuConstants.ExcludePaths
                    }
                };
            }
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var cookie = new Cookie("region_position_nn", _city.CookieValue, "/", ".holodilnik.ru");
            request.Cookies.Add(cookie);
            return base.LoadPageContent(request, context);
        }
    }
}