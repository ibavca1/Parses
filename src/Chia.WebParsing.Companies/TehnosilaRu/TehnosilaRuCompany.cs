using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.TehnosilaRu
{
    public class TehnosilaRuCompany : WebCompany
    {
        public static readonly TehnosilaRuCompany Instance = new TehnosilaRuCompany();

        private TehnosilaRuCompany()
        {
        }

        public override int Id
        {
            get { return TehnosilaRuConstants.Id; }
        }

        public override string Name
        {
            get { return TehnosilaRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(TehnosilaRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = TehnosilaRuConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            TehnosilaRuCity c = TehnosilaRuCity.Get(city);
            return new TehnosilaRuWebSite(c);
        }
    }
}