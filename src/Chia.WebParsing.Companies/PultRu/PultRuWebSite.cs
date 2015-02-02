using System;
using System.Net;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.PultRu
{
    public class PultRuWebSite : WebSite
    {
        private readonly PultRuCity _city;

        public PultRuWebSite(PultRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return PultRuCompany.Instance; }
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
            get { return Encoding.UTF8; }
        }

        public override WebSiteMetadata Metadata
        {
            get
            {
                return new WebSiteMetadata
                {
                    ProductActicle = PultRuConstants.SupportArticles,
                    PriceTypes = PultRuConstants.SupportedPriceTypes,
                    ProxyTimeout = PultRuConstants.ProxyTimeout,
                    AvailabilityInShops = PultRuConstants.SupportShopsAvailability,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = PultRuConstants.ExcludeKeywords,
                        ExcludePaths = PultRuConstants.ExcludePaths
                    }
                };
            }
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var cookie = new Cookie("curr_city", _city.CookieValue, "/", "pult.ru");
            request.Cookies.Add(cookie);
            return base.LoadPageContent(request, context);
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if ((PultRuWebPageType)type == PultRuWebPageType.Catalog)
            {
                uri = uri
                    .AddQueryParam("avail[av]", "1") // выводим только то, что в наличии
                    .AddQueryParam("avail[or]", "1") // и под заказ
                    .AddQueryParam("kol", "90"); // выводим по 60 товаров
            }

            return base.GetPage(uri, type, path);
        }
    }
}