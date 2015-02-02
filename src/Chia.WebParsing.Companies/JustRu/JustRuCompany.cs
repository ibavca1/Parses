using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.JustRu
{
    public class JustRuCompany : WebCompany
    {
        public static readonly JustRuCompany Instance = new JustRuCompany();

        private JustRuCompany()
        {
        }

        public override int Id
        {
            get { return JustRuConstants.Id; }
        }

        public override string Name
        {
            get { return JustRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(JustRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = JustRuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            JustRuCity c = JustRuCity.Get(city);
            return new JustRuWebSite(c);
        }
    }
}