using System;
using System.Net;
using System.Text;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.SvyaznoyRu
{
    public class SvyaznoyRuWebSite : WebSite
    {
        private readonly SvyaznoyRuCity _city;

        public SvyaznoyRuWebSite(SvyaznoyRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return SvyaznoyRuCompany.Instance; }
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
                    ProductActicle = SvyaznoyRuConstants.SupportArticles,
                    PriceTypes = SvyaznoyRuConstants.PriceTypes,
                    ProxyTimeout = SvyaznoyRuConstants.ProxyTimeout,
                    AvailabilityInShops = SvyaznoyRuConstants.SupportAvailabilityInShops,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = SvyaznoyRuConstants.ExcludeKeywords,
                        ExcludePaths = SvyaznoyRuConstants.ExcludePaths
                    }
                };
            }
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var cityCookie = new Cookie("SETCITY", _city.CityId, "/", ".svyaznoy.ru");
            request.Cookies.Add(cityCookie);

            if (SvyaznoyRuWebPageType.Catalog == (SvyaznoyRuWebPageType)request.Type)
            {
                var pageSizeCookie = new Cookie("PAGE_SIZE", "100", "/", "www.svyaznoy.ru");
                request.Cookies.Add(pageSizeCookie);
            }

            return base.LoadPageContent(request, context);
        }
    }
}