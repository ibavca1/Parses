using System;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.ElectrovenikRu
{
    public class ElectrovenikRuWebSite : WebSite
    {
        public const string UriMask = ElectrovenikRuConstants.UriMask;

        private readonly ElectrovenikRuCity _city;

        public ElectrovenikRuWebSite(ElectrovenikRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return ElectrovenikRuCompany.Instance; }
        }

        public override WebCity City
        {
            get { return _city; }
        }

        public override Uri Uri
        {
            get
            {
                string address = string.Format(UriMask, _city.UriPrefix);
                return new Uri(address);
            }
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        public override WebSiteMetadata Metadata
        {
            get
            {
                return new WebSiteMetadata
                {
                    ProductActicle = ElectrovenikRuConstants.Articles,
                    PriceTypes = ElectrovenikRuConstants.PriceTypes,
                    ProxyTimeout = ElectrovenikRuConstants.ProxyTimeout,
                    AvailabilityInShops = ElectrovenikRuConstants.ShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = ElectrovenikRuConstants.ExcludeKeywords,
                        ExcludePaths = ElectrovenikRuConstants.ExcludePaths
                    }
                };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if ((ElectrovenikRuWebPageType)type == ElectrovenikRuWebPageType.Catalog)
            {
                uri = uri.AddQueryParam("sort", "price");
            }

            return base.GetPage(uri, type, path);
        }
    }
}