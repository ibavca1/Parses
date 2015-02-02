using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.CorpCentreRu
{
    public class CorpCentreRuCompany : WebCompany
    {
        public static readonly CorpCentreRuCompany Instance = new CorpCentreRuCompany();

        public override int Id
        {
            get { return CorpCentreRuConstants.Id; }
        }

        public override string Name
        {
            get { return CorpCentreRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(CorpCentreRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = CorpCentreRuConstants.Cities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            CorpCentreRuCity c = CorpCentreRuCity.Get(city);
            return new CorpCentreRuWebSite(c);
        }
    }
}