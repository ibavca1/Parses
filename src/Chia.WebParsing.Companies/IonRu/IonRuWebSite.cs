using System;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.IonRu
{
    public class IonRuWebSite : WebSite
    {
        private readonly WebCity _city;

        public IonRuWebSite(WebCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return IonRuCompany.Instance; }
        }

        public override WebCity City
        {
            get { return _city; }
        }

        public override Uri Uri
        {
            get { return Company.SiteUri; }
        }

        public override Encoding Encoding
        {
            get { return IonRuConstants.Encoding; }
        }

        public override WebSiteMetadata Metadata
        {
            get
            {
                return new WebSiteMetadata
                {
                    ProductActicle = IonRuConstants.Articles,
                    PriceTypes = IonRuConstants.PriceTypes,
                    ProxyTimeout = IonRuConstants.ProxyTimeout,
                    AvailabilityInShops = IonRuConstants.ShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = IonRuConstants.ExcludeKeywords,
                        ExcludePaths = IonRuConstants.ExcludePaths
                    }
                };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if ((IonRuWebPageType)type == IonRuWebPageType.Catalog)
            {
                uri = uri.AddQueryParam("available", "true");
            }
            else if ((IonRuWebPageType)type == IonRuWebPageType.AvailabilityInShops)
            {
                uri = uri.Append("remains");
            }

            return base.GetPage(uri, type, path);
        }
    }
}