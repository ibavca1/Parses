using System;
using System.Net;
using System.Text;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.Oo3Ru
{
    public class Oo3RuWebSite : WebSite
    {
        private readonly Oo3RuCity _city;

        public Oo3RuWebSite(Oo3RuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return Oo3RuCompany.Instance; }
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
                    ProductActicle = Oo3RuConstants.SupportsProductArticle,
                    PriceTypes = Oo3RuConstants.PriceTypes,
                    ProxyTimeout = Oo3RuConstants.ProxyTimeout,
                    AvailabilityInShops = Oo3RuConstants.SupportsAvailabilityInShops,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = Oo3RuConstants.ExcludeKeywords,
                        ExcludePaths = Oo3RuConstants.ExcludePaths
                    }
                };
            }
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var cookies = new CookieCollection
                                {
                                    new Cookie("geo_ip_current_city_id_003", _city.CityId.ToString(), "/", ".003.ru"),
                                    new Cookie("geo_ip_region_id_003", _city.RegionId.ToString(), "/", ".003.ru"),
                                    new Cookie("geo_ip_region_003", _city.RegionCode, "/", ".003.ru"),
                                    new Cookie("geo_ip_town_003", _city.CityCode, "/", ".003.ru"),
                                };
            request.Cookies.Add(cookies);
            
            return base.LoadPageContent(request, context);
        }
    }
}