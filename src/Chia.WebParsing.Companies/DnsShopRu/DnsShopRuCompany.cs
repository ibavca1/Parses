using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.DnsShopRu
{
    public class DnsShopRuCompany : WebCompany
    {
        public static readonly DnsShopRuCompany Instance = new DnsShopRuCompany();

        private DnsShopRuCompany()
        {
        }

        public override int Id
        {
            get { return DnsShopRuConstants.Id; }
        }

        public override string Name
        {
            get { return DnsShopRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(DnsShopRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata { Cities = DnsShopRuConstants.SupportedCities }; }
        }

        public override WebSite GetSite(WebCity city)
        {
            DnsShopRuCity c = DnsShopRuCity.Get(city);
            return new DnsShopRuWebSite(c);
        }
    }
}