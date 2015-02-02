using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.OzonRu
{
    public class OzonRuCompany : WebCompany
    {
        public static readonly OzonRuCompany Instance = new OzonRuCompany();

        private OzonRuCompany()
        {
        }

        public override int Id
        {
            get { return OzonRuConstants.Id; }
        }

        public override string Name
        {
            get { return OzonRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(OzonRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = OzonRuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            return new OzonRuWebSite(city);
        }
    }
}