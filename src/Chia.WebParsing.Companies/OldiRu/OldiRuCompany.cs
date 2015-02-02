using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.OldiRu
{
    public class OldiRuCompany : WebCompany
    {
        public static readonly OldiRuCompany Instance = new OldiRuCompany();

        private OldiRuCompany()
        {
        }

        public override int Id
        {
            get { return OldiRuConstants.Id; }
        }

        public override string Name
        {
            get { return OldiRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(OldiRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = OldiRuConstants.Cities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            OldiRuCity c = OldiRuCity.Get(city);
            return new OldiRuWebSite(c);
        }
    }
}