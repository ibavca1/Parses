using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.DostavkaRu
{
    public class DostavkaRuCompany : WebCompany
    {
        public static readonly DostavkaRuCompany Instance = new DostavkaRuCompany();

        private DostavkaRuCompany()
        {
        }

        public override int Id
        {
            get { return DostavkaRuConstants.Id; }
        }

        public override string Name
        {
            get { return DostavkaRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(DostavkaRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = DostavkaRuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            return new DostavkaRuWebSite(city);
        }
    }
}