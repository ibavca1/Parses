using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.TdPoiskRu
{
    public class TdPoiskRuCompany : WebCompany
    {
        public static readonly TdPoiskRuCompany Instance = new TdPoiskRuCompany();

        public override int Id
        {
            get { return TdPoiskRuConstants.Id; }
        }

        public override string Name
        {
            get { return TdPoiskRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(TdPoiskRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = TdPoiskRuConstants.Cities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            TdPoiskRuCity c = TdPoiskRuCity.Get(city);
            return new TdPoiskRuWebSite(c);
        }
    }
}