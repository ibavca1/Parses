using System;
using System.Text;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.TenIRu
{
    public class TenIRuWebSite : WebSite
    {
        private readonly TenIRuCity _city;

        public TenIRuWebSite(TenIRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return TenIRuCompany.Instance; }
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
                    ProductActicle = TenIRuConstants.Articles,
                    PriceTypes = TenIRuConstants.PriceTypes,
                    ProxyTimeout = TenIRuConstants.ProxyTimeout,
                    AvailabilityInShops = TenIRuConstants.ShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = TenIRuConstants.ExcludeKeywords,
                        ExcludePaths = TenIRuConstants.ExcludePaths
                    }
                };
            }
        }
    }
}