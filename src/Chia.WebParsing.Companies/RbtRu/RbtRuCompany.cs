using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.RbtRu
{
    public class RbtRuCompany : WebCompany
    {
        public static readonly RbtRuCompany Instance = new RbtRuCompany();

        private RbtRuCompany()
        {
        }

        public override int Id
        {
            get { return RbtRuConstants.Id; }
        }

        public override string Name
        {
            get { return RbtRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(RbtRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = RbtRuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            RbtRuCity c = RbtRuCity.Get(city);
            return new RbtRuWebSite(c);
        }
    }
}