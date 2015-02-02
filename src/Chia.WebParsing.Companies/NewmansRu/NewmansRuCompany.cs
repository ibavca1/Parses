using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.NewmansRu
{
    public class NewmansRuCompany : WebCompany
    {
        public static readonly NewmansRuCompany Instance = new NewmansRuCompany();

        public override int Id
        {
            get { return NewmansRuConstants.Id; }
        }

        public override string Name
        {
            get { return NewmansRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(NewmansRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = NewmansRuConstants.Cities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            NewmansRuCity c = NewmansRuCity.Get(city);
            return new NewmansRuWebSite(c);
        }
    }
}