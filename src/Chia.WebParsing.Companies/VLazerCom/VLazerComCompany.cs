using System;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.VLazerCom
{
    public class VLazerComCompany : WebCompany
    {
        public static readonly VLazerComCompany Instance = new VLazerComCompany();

        private VLazerComCompany()
        {
        }

        public override int Id
        {
            get { return VLazerComConstants.Id; }
        }

        public override string Name
        {
            get { return VLazerComConstants.Name; }
        }

        public override Uri SiteUri
        {
            get { return new Uri(VLazerComConstants.SiteUri); }
        }

        public override WebCompanyMetadata Metadata
        {
            get { return new WebCompanyMetadata {Cities = VLazerComConstants.SupportedCities}; }
        }

        public override WebSite GetSite(WebCity city)
        {
            VLazerComCity c = VLazerComCity.Get(city);
            return new VLazerComWebSite(c);
        }
    }
}