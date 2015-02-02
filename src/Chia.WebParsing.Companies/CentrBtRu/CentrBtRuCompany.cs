using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.CentrBtRu
{
    public class CentrBtRuCompany : WebCompany
    {
        public static readonly CentrBtRuCompany Instance = new CentrBtRuCompany();

        public override int Id
        {
            get { return CentrBtRuConstants.Id; }
        }

        public override string Name
        {
            get { return CentrBtRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(CentrBtRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = CentrBtRuConstants.Cities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            CentrBtRuCity c = CentrBtRuCity.Get(city);
            return new CentrBtRuWebSite(c);
        }
    }
}