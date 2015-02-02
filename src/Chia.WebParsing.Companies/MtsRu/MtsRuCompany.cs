using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.MtsRu
{
    public class MtsRuCompany : WebCompany
    {
        public static readonly MtsRuCompany Instance = new MtsRuCompany();

        private MtsRuCompany()
        {
        }

        public override int Id
        {
            get { return MtsRuConstants.Id; }
        }

        public override string Name
        {
            get { return MtsRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(MtsRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = MtsRuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            MtsRuCity c = MtsRuCity.Get(city);
            return new MtsRuWebSite(c);
        }
    }
}