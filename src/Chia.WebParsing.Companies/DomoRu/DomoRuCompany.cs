using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.DomoRu
{
    public class DomoRuCompany : WebCompany
    {
        public static readonly DomoRuCompany Instance = new DomoRuCompany();

        private DomoRuCompany()
        {
        }

        public override int Id
        {
            get { return DomoRuConstants.Id; }
        }

        public override string Name
        {
            get { return DomoRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(DomoRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = DomoRuConstants.Cities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            DomoRuCity c = DomoRuCity.Get(city);
            return new DomoRuWebSite(c);
        }
    }
}