using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.Oo3Ru
{
    public class Oo3RuCompany : WebCompany
    {
        public static readonly Oo3RuCompany Instance = new Oo3RuCompany();

        private Oo3RuCompany()
        {
        }

        public override int Id
        {
            get { return Oo3RuConstants.Id; }
        }

        public override string Name
        {
            get { return Oo3RuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(Oo3RuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = Oo3RuConstants.Cities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            Oo3RuCity c = Oo3RuCity.Get(city);
            return new Oo3RuWebSite(c);
        }
    }
}