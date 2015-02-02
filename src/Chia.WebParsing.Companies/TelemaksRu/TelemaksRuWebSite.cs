using System;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.TelemaksRu
{
    public class TelemaksRuWebSite : WebSite
    {
        private readonly TelemaksRuCity _city;
        private const string UriMask = TelemaksRuConstants.SiteUriMask;

        public TelemaksRuWebSite(TelemaksRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return TelemaksRuCompany.Instance; }
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
                               ProductActicle = TelemaksRuConstants.SupportArticles,
                               PriceTypes = TelemaksRuConstants.SupportedPriceTypes,
                               ProxyTimeout = TelemaksRuConstants.ProxyTimeout,
                               AvailabilityInShops = TelemaksRuConstants.SupportShopsAvailability,
                               PagesFilter = new WebPagesFilter
                                                 {
                                                     ExcludeKeywords = TelemaksRuConstants.ExcludeKeywords,
                                                     ExcludePaths = TelemaksRuConstants.ExcludePaths
                                                 }
                           };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if ((TelemaksRuWebPageType)type == TelemaksRuWebPageType.Catalog)
            {
                uri = uri.AddQueryParam("sort", "1");
            }

            return base.GetPage(uri, type, path);
        }
    }
}