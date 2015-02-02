using System;
using System.Net;
using System.Text;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.CorpCentreRu
{
    public class CorpCentreRuWebSite : WebSite
    {
        private readonly CorpCentreRuCity _city;

        public CorpCentreRuWebSite(CorpCentreRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return CorpCentreRuCompany.Instance; }
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
                    ProductActicle = CorpCentreRuConstants.Articles,
                    PriceTypes = CorpCentreRuConstants.PriceTypes,
                    ProxyTimeout = CorpCentreRuConstants.ProxyTimeout,
                    AvailabilityInShops = CorpCentreRuConstants.ShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = CorpCentreRuConstants.ExcludeKeywords,
                        ExcludePaths = CorpCentreRuConstants.ExcludePaths
                    }
                };
            }
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var cookie = new Cookie("BITRIX_SM_STORE_ID", _city.CookieValue, "/", "www.corpcentre.ru");
            request.Cookies.Add(cookie);
            return base.LoadPageContent(request, context);
        }
    }
}