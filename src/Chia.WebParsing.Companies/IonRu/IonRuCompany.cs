using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.IonRu
{
    public class IonRuCompany : WebCompany
    {
        public static readonly IonRuCompany Instance = new IonRuCompany();

        public override int Id
        {
            get { return IonRuConstants.Id; }
        }

        public override string Name
        {
            get { return IonRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(IonRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata{Cities = IonRuConstants.Cities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            return new IonRuWebSite(city);
        }
    }
}