using System;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.OldiRu
{
    public class OldiRuWebSite : WebSite
    {
        private readonly OldiRuCity _city;

        public OldiRuWebSite(OldiRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return OldiRuCompany.Instance; }
        }

        public override WebCity City
        {
            get { return _city; }
        }

        public override Uri Uri
        {
            get
            {
                Uri siteUri = Company.SiteUri;
                Uri uri = siteUri.AddQueryParam("selectCityID", _city.InternalId);
                return uri;
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
                    ProductActicle = OldiRuConstants.Articles,
                    PriceTypes = OldiRuConstants.PriceTypes,
                    ProxyTimeout = OldiRuConstants.ProxyTimeout,
                    AvailabilityInShops = OldiRuConstants.ShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = OldiRuConstants.ExcludeKeywords,
                        ExcludePaths = OldiRuConstants.ExcludePaths
                    }
                };
            }
        }
    }
}