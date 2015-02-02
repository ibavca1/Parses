using System;
using System.Net;
using System.Text;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.EurosetRu
{
    public class EurosetRuWebSite : WebSite
    {
        private readonly EurosetRuCity _city;

        public EurosetRuWebSite(EurosetRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return EurosetRuCompany.Instance; }
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
                    ProductActicle = EurosetRuConstants.SupportArticles,
                    PriceTypes = EurosetRuConstants.SupportedPriceTypes,
                    ProxyTimeout = EurosetRuConstants.ProxyTimeout,
                    AvailabilityInShops = EurosetRuConstants.SupportShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = EurosetRuConstants.ExcludeKeywords,
                        ExcludePaths = EurosetRuConstants.ExcludePaths
                    }
                };
            }
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var cookie = new Cookie("GEO_LOC_ID", _city.CookieValue, "/", "euroset.ru");
            request.Cookies.Add(cookie);
            return base.LoadPageContent(request, context);
        }
    }
}