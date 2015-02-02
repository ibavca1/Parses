using System;
using System.Net;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.TechnonetRu
{
    public class TechnonetRuWebSite : WebSite
    {
        private readonly TechnonetRuCity _city;

        public TechnonetRuWebSite(TechnonetRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return TechnonetRuCompany.Instance; }
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
                    ProductActicle = TechnonetRuConstants.Articles,
                    PriceTypes = TechnonetRuConstants.PriceTypes,
                    ProxyTimeout = TechnonetRuConstants.ProxyTimeout,
                    AvailabilityInShops = TechnonetRuConstants.ShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = TechnonetRuConstants.ExcludeKeywords,
                        ExcludePaths = TechnonetRuConstants.ExcludePaths
                    }
                };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {

            if ((TechnonetRuWebPageType)type == TechnonetRuWebPageType.Catalog)
            {
                uri = uri
                    .AddQueryParam("view", "72")
                    .AddQueryParam("sort", "name")
                    .AddQueryParam("order", "asc");
            }

            return base.GetPage(uri, type, path);
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var cookie = new Cookie("BITRIX_SM_cityId", _city.CookieValue, "/", "www.Technonet.ru");
            request.Cookies.Add(cookie);
            return base.LoadPageContent(request, context);
        }
    }
}