using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.LogoRu
{
    public class LogoRuCompany : WebCompany
    {
        public static readonly LogoRuCompany Instance = new LogoRuCompany();

        private LogoRuCompany()
        {
        }

        public override int Id
        {
            get { return LogoRuConstants.Id; }
        }

        public override string Name
        {
            get { return LogoRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(LogoRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = LogoRuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            LogoRuCity c = LogoRuCity.Get(city);
            return new LogoRuWebSite(c);
        }
    }
}