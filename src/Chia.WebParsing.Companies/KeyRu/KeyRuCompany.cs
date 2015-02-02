using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.KeyRu
{
    public class KeyRuCompany : WebCompany
    {
        public static readonly KeyRuCompany Instance = new KeyRuCompany();

        private KeyRuCompany()
        {
        }

        public override int Id
        {
            get { return KeyRuConstants.Id; }
        }

        public override string Name
        {
            get { return KeyRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(KeyRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata { Cities = KeyRuConstants.Cities }; }
        }

        public override WebSite GetSite(WebCity city)
        {
            KeyRuCity c = KeyRuCity.Get(city);
            return new KeyRuWebSite(c);
        }
    }
}