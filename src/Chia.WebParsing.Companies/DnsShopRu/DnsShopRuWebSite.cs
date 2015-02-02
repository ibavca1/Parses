using System;
using System.Net;
using System.Text;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Companies.DnsShopRu
{
    public class DnsShopRuWebSite : WebSite
    {
        private readonly DnsShopRuCity _city;

        public DnsShopRuWebSite(DnsShopRuCity city)
        {
            _city = city;
        }

        public override WebCompany Company
        {
            get { return DnsShopRuCompany.Instance; }
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
                    ProductActicle = DnsShopRuConstants.ProductsArticles,
                    PriceTypes = DnsShopRuConstants.SupportedPriceTypes,
                    ProxyTimeout = DnsShopRuConstants.ProxyTimeout,
                    AvailabilityInShops = DnsShopRuConstants.AvailabilityInShops,
                    PagesFilter = new WebPagesFilter
                    {
                        ExcludeKeywords = DnsShopRuConstants.ExcludeKeywords,
                        ExcludePaths = DnsShopRuConstants.ExcludePaths
                    }
                };
            }
        }

        protected override WebPage GetPage(Uri uri, WebPageType type, WebSiteMapPath path)
        {
            if ((DnsShopRuWebPageType)type == DnsShopRuWebPageType.Catalog)
            {
                uri = uri
                    .AddQueryParam("length_1", "200") // по 200 товаров на странице 
                    .AddQueryParam("order", "3"); // сортировка по наименованию
            }

            return base.GetPage(uri, type, path);
        }

        public override WebPageContent LoadPageContent(WebPageRequest request, WebPageContentParsingContext context)
        {
            var cookie = new Cookie("city_path", _city.CookieValue, "/", ".dns-shop.ru");
            request.Cookies.Add(cookie);

            return base.LoadPageContent(request, context);
        }
    }
}