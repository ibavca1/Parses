using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.TechportRu
{
    public class TechportRuCompany : WebCompany
    {
        public static readonly TechportRuCompany Instance = new TechportRuCompany();

        private TechportRuCompany()
        {
        }

        public override int Id
        {
            get { return TechportRuConstants.Id; }
        }

        public override string Name
        {
            get { return TechportRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(TechportRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = TechportRuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            TechportRuCity c = TechportRuCity.Get(city);
            return new TechportRuWebSite(c);
        }
    }
}