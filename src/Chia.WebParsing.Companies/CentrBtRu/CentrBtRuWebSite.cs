using System;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.CentrBtRu
{
    public class CentrBtRuWebSite : WebSite
    {
        private readonly CentrBtRuCity _city;

        public CentrBtRuWebSite(CentrBtRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return CentrBtRuCompany.Instance; }
        }

        public override WebCity City
        {
            get { return _city; }
        }

        public override Uri Uri
        {
            get
            {
                return Company.SiteUri.AddQueryParam("df_my_city", _city.InternalId);
            }
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
                    ProductActicle = CentrBtRuConstants.Articles,
                    PriceTypes = CentrBtRuConstants.PriceTypes,
                    ProxyTimeout = CentrBtRuConstants.ProxyTimeout,
                    AvailabilityInShops = CentrBtRuConstants.ShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = CentrBtRuConstants.ExcludeKeywords,
                        ExcludePaths = CentrBtRuConstants.ExcludePaths
                    }
                };
            }
        }
    }
}