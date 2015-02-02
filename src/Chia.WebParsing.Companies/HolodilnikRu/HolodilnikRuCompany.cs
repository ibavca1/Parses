using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.HolodilnikRu
{
    public class HolodilnikRuCompany : WebCompany
    {
        public static readonly HolodilnikRuCompany Instance = new HolodilnikRuCompany();

        private HolodilnikRuCompany()
        {
        }

        public override int Id
        {
            get { return HolodilnikRuConstants.Id; }
        }

        public override string Name
        {
            get { return HolodilnikRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(HolodilnikRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = HolodilnikRuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            HolodilnikRuCity c = HolodilnikRuCity.Get(city);
            return new HolodilnikRuWebSite(c);
        }
    }
}