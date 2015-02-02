using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.TehnoparkRu
{
    public class TehnoparkRuCompany : WebCompany
    {
        public static readonly TehnoparkRuCompany Instance = new TehnoparkRuCompany();

        private TehnoparkRuCompany()
        {
        }

        public override int Id
        {
            get { return TehnoparkRuConstants.Id; }
        }

        public override string Name
        {
            get { return TehnoparkRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(TehnoparkRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = TehnoparkRuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            TehnoparkRuCity c = TehnoparkRuCity.Get(city);
            return new TehnoparkRuWebSite(c);
        }
    }
}