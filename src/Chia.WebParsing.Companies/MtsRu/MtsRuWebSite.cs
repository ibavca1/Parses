using System;
using System.Net;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.MtsRu
{
    public class MtsRuWebSite : WebSite
    {
        private readonly MtsRuCity _city;

        public MtsRuWebSite(MtsRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return MtsRuCompany.Instance; }
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
            get { return Encoding.GetEncoding(1251); }
        }

        public override WebSiteMetadata Metadata
        {
            get
            {
                return new WebSiteMetadata
                {
                    ProductActicle = MtsRuConstants.SupportArticles,
                    PriceTypes = MtsRuConstants.SupportedPriceTypes,
                    ProxyTimeout = MtsRuConstants.ProxyTimeout,
                    AvailabilityInShops = MtsRuConstants.SupportShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = MtsRuConstants.ExcludeKeywords,
                        ExcludePaths = MtsRuConstants.ExcludePaths
                    }
                };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if ((MtsRuWebPageType)type == MtsRuWebPageType.Catalog)
            {
                uri = uri.AddQueryParam("orderby", "price_desc");
            }

            return base.GetPage(uri, type, path);
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var cookie = new Cookie("GeoCookie", _city.CookieValue, "/", ".shop.mts.ru");
            request.Cookies.Add(cookie);
            return base.LoadPageContent(request, context);
        }
    }
}