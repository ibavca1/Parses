using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.Nord24Ru
{
    public class Nord24RuCompany : WebCompany
    {
        public static readonly Nord24RuCompany Instance = new Nord24RuCompany();

        private Nord24RuCompany()
        {
        }

        public override int Id
        {
            get { return Nord24RuConstants.Id; }
        }

        public override string Name
        {
            get { return Nord24RuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(Nord24RuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = Nord24RuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            Nord24RuCity c = Nord24RuCity.Get(city);
            return new Nord24RuWebSite(c);
        }
    }
}