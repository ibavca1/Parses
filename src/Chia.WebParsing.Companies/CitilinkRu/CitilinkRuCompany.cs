using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.CitilinkRu
{
    public class CitilinkRuCompany : WebCompany
    {
        public static readonly CitilinkRuCompany Instance = new CitilinkRuCompany();

        private CitilinkRuCompany()
        {
        }

        public override int Id
        {
            get { return CitilinkRuConstants.Id; }
        }

        public override string Name
        {
            get { return CitilinkRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(CitilinkRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = CitilinkRuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            CitilinkRuCity c = CitilinkRuCity.Get(city);
            return new CitilinkRuWebSite(c);
        }
    }
}