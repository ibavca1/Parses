using System;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.NotikRu
{
    public class NotikRuWebSite : WebSite
    {
        private readonly NotikRuCity _city;

        public NotikRuWebSite(NotikRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return NotikRuCompany.Instance; }
        }

        public override WebCity City
        {
            get { return _city; }
        }

        public override Uri Uri
        {
            get
            {
                Uri uri = new Uri(NotikRuConstants.MobileSiteUri).AddQueryParam("cityid", _city.Code);
                return uri;
            }
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
                    ProductActicle = NotikRuConstants.SupportArticles,
                    PriceTypes = NotikRuConstants.PriceTypes,
                    ProxyTimeout = NotikRuConstants.ProxyTimeout,
                    AvailabilityInShops = NotikRuConstants.SupportShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = NotikRuConstants.ExcludeKeywords,
                        ExcludePaths = NotikRuConstants.ExcludePaths
                    }
                };
            }
        }
    }
}