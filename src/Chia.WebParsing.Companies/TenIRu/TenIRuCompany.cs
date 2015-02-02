using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.TenIRu
{
    public class TenIRuCompany : WebCompany
    {
        public static readonly TenIRuCompany Instance = new TenIRuCompany();

        private TenIRuCompany()
        {
        }

        public override int Id
        {
            get { return TenIRuConstants.Id; }
        }

        public override string Name
        {
            get { return TenIRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(TenIRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = TenIRuConstants.Cities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            TenIRuCity c = TenIRuCity.Get(city);
            return new TenIRuWebSite(c);
        }
    }
}