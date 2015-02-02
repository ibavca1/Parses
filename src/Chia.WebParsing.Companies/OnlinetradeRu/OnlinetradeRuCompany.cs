using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.OnlinetradeRu
{
    public class OnlinetradeRuCompany : WebCompany
    {
        public static readonly OnlinetradeRuCompany Instance = new OnlinetradeRuCompany();

        private OnlinetradeRuCompany()
        {
        }

        public override int Id
        {
            get { return OnlinetradeRuConstants.Id; }
        }

        public override string Name
        {
            get { return OnlinetradeRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(OnlinetradeRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = OnlinetradeRuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            OnlinetradeRuCity c = OnlinetradeRuCity.Get(city);
            return new OnlinetradeRuWebSite(c);
        }
    }
}