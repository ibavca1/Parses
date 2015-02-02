using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.EldoradoRu
{
    public class EldoradoRuCompany : WebCompany
    {
        public static readonly EldoradoRuCompany Instance = new EldoradoRuCompany();

        private EldoradoRuCompany()
        {
        }

        public override int Id
        {
            get { return EldoradoRuConstants.Id; }
        }

        public override string Name
        {
            get { return EldoradoRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(EldoradoRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = EldoradoRuConstants.Cities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            EldoradoRuCity c = EldoradoRuCity.Get(city);
            return new EldoradoRuWebSite(c);
        }
    }
}