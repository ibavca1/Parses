using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.TechnonetRu
{
    public class TechnonetRuCompany : WebCompany
    {
        public static readonly TechnonetRuCompany Instance = new TechnonetRuCompany();

        public override int Id
        {
            get { return TechnonetRuConstants.Id; }
        }

        public override string Name
        {
            get { return TechnonetRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(TechnonetRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = TechnonetRuConstants.Cities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            TechnonetRuCity c = TechnonetRuCity.Get(city);
            return new TechnonetRuWebSite(c);
        }
    }
}