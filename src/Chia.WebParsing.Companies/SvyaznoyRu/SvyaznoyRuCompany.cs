using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.SvyaznoyRu
{
    public class SvyaznoyRuCompany : WebCompany
    {
        public static readonly SvyaznoyRuCompany Instance = new SvyaznoyRuCompany();

        private SvyaznoyRuCompany()
        {
        }

        public override int Id
        {
            get { return SvyaznoyRuConstants.Id; }
        }

        public override string Name
        {
            get { return SvyaznoyRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(SvyaznoyRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = SvyaznoyRuConstants.Cities}; }
        }
        
        public override WebSite GetSite(WebCity city)
        {
            SvyaznoyRuCity c = SvyaznoyRuCity.Get(city);
            return new SvyaznoyRuWebSite(c);
        }
    }
}