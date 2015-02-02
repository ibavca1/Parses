using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.PultRu
{
    public class PultRuCompany : WebCompany
    {
        public static readonly PultRuCompany Instance = new PultRuCompany();

        private PultRuCompany()
        {
        }

        public override int Id
        {
            get { return PultRuConstants.Id; }
        }

        public override string Name
        {
            get { return PultRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(PultRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = PultRuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            PultRuCity c = PultRuCity.Get(city);
            return new PultRuWebSite(c);
        }
    }
}