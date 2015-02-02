using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.ElectrovenikRu
{
    public class ElectrovenikRuCompany : WebCompany
    {
        public static readonly ElectrovenikRuCompany Instance = new ElectrovenikRuCompany();

        private ElectrovenikRuCompany()
        {
        }

        public override int Id
        {
            get { return ElectrovenikRuConstants.Id; }
        }

        public override string Name
        {
            get { return ElectrovenikRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(ElectrovenikRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = ElectrovenikRuConstants.Cities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            ElectrovenikRuCity c = ElectrovenikRuCity.Get(city);
            return new ElectrovenikRuWebSite(c);
        }
    }
}