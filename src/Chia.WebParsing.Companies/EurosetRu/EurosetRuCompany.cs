using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.EurosetRu
{
    public class EurosetRuCompany : WebCompany
    {
        public static readonly EurosetRuCompany Instance = new EurosetRuCompany();

        private EurosetRuCompany()
        {
        }

        public override int Id
        {
            get { return EurosetRuConstants.Id; }
        }

        public override string Name
        {
            get { return EurosetRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(EurosetRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = EurosetRuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            EurosetRuCity c = EurosetRuCity.Get(city);
            return new EurosetRuWebSite(c);
        }
    }
}