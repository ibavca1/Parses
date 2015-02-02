using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.RegardRu
{
    public class RegardRuCompany : WebCompany
    {
        public static readonly RegardRuCompany Instance = new RegardRuCompany();

        private RegardRuCompany()
        {
        }

        public override int Id
        {
            get { return RegardRuConstants.Id; }
        }

        public override string Name
        {
            get { return RegardRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(RegardRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = RegardRuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            RegardRuCity c = RegardRuCity.Get(city);
            return new RegardRuWebSite(c);
        }
    }
}