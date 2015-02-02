using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.EnterRu
{
    public class EnterRuCompany : WebCompany
    {
        public static readonly EnterRuCompany Instance = new EnterRuCompany();

        private EnterRuCompany()
        {
        }

        public override int Id
        {
            get { return EnterRuConstants.Id; }
        }

        public override string Name
        {
            get { return EnterRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(EnterRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = EnterRuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            EnterRuCity c = EnterRuCity.Get(city);
            return new EnterRuWebSite(c);
        }
    }
}