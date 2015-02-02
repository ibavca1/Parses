using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.TehnoshokRu
{
    public class TehnoshokRuCompany : WebCompany
    {
        public static readonly TehnoshokRuCompany Instance = new TehnoshokRuCompany();

        private TehnoshokRuCompany()
        {
        }

        public override int Id
        {
            get { return TehnoshokRuConstants.Id; }
        }

        public override string Name
        {
            get { return TehnoshokRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(TehnoshokRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = TehnoshokRuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            TehnoshokRuCity c = TehnoshokRuCity.Get(city);
            return new TehnoshokRuWebSite(c);
        }
    }
}