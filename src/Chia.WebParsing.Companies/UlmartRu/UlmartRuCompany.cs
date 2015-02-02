using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.UlmartRu
{
    public class UlmartRuCompany : WebCompany
    {
        public static readonly UlmartRuCompany Instance = new UlmartRuCompany();

        private UlmartRuCompany()
        {
        }

        public override int Id
        {
            get { return UlmartRuConstants.Id; }
        }

        public override string Name
        {
            get { return UlmartRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(UlmartRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = UlmartRuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            UlmartRuCity c = UlmartRuCity.Get(city);
            return new UlmartRuWebSite(c);
        }
    }
}