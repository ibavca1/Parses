using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.NotikRu
{
    public class NotikRuCompany : WebCompany
    {
        public static readonly NotikRuCompany Instance = new NotikRuCompany();

        private NotikRuCompany()
        {
        }

        public override int Id
        {
            get { return NotikRuConstants.Id; }
        }

        public override string Name
        {
            get { return NotikRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(NotikRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = NotikRuConstants.Cities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            NotikRuCity c = NotikRuCity.Get(city);
            return new NotikRuWebSite(c);
        }
    }
}