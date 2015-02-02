using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.MvideoRu
{
    public class MvideoRuCompany : WebCompany
    {
        public static readonly MvideoRuCompany Instance = new MvideoRuCompany();

        private MvideoRuCompany()
        {
        }

        public override int Id
        {
            get { return MvideoRuConstants.Id; }
        }

        public override string Name
        {
            get { return MvideoRuConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(MvideoRuConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = MvideoRuConstants.Cities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            MvideoRuCity mvCity = MvideoRuCity.Get(city);
            return new MvideoRuWebSite(mvCity);
        }
    }
}