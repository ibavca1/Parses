using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.UtinetRu
{
    public class UtinetRuCompany : WebCompany
    {
        public static readonly UtinetRuCompany Instance = new UtinetRuCompany();

        private UtinetRuCompany()
        {
        }

        public override int Id
        {
            get { return UtinetRuConstants.Id; }
        }

        public override string Name
        {
            get { return UtinetRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(UtinetRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = UtinetRuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            UtinetRuCity c = UtinetRuCity.Get(city);
            return new UtinetRuWebSite(c);
        }
    }
}