using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.TelemaksRu
{
    public class TelemaksRuCompany : WebCompany
    {
        public static readonly TelemaksRuCompany Instance = new TelemaksRuCompany();

        private TelemaksRuCompany()
        {
        }

        public override int Id
        {
            get { return TelemaksRuConstants.Id; }
        }

        public override string Name
        {
            get { return TelemaksRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(TelemaksRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = TelemaksRuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            TelemaksRuCity c = TelemaksRuCity.Get(city);
            return new TelemaksRuWebSite(c);
        }
    }
}